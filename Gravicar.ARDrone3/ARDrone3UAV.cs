using System;
using System.Net.Sockets;

// Глянуть, нет ли в .NET Standard базового класса для UdpClient и TcpClient
//using Newtonsoft.Json.Linq;

using Gravicar.ARDrone3.Commands;

namespace Gravicar.ARDrone3
{
    public abstract class ARDrone3UAV : UAV
    {
        protected ARDrone3UAV () : base (null)
        {
        }

        /// <summary>
        /// Выполняет первоначальный поиск дрона при установке сетевого соединения.
        /// </summary>
        /// <param name="responseJson">JSON,возвращаемый дроном. Если при установке соединения или записи / чтении данных произошла 
        /// ошибка, <paramref name="responseJson"/> будет равен String.Empty. Подробнее о формате JSON’ов, пересылаемых при поиске 
        /// дрона и установке первичного соединения, см. ARSDK_Protocols.pdf, раздел 1.2.1.</param>
        /// <returns><list type="bullet">
        /// <item>TRUE – операция прошла успешно, соединение установлено корректно.</item>
        /// <item>FALSE – что-то пошло не так.Возможные причины:<list type="bullet">
        ///         <item>не установлено соединение;</item>
        ///         <item>произошла ошибка во время передачи или приёма данных;</item>
        ///         <item>поле status в JSON-запросе, пришедшем от дрона, не равно Const.Discovering.VToC_ConnectionIsCorrect"/>.</item>
        ///     </list></item>
        /// </list></returns>
        public bool Discover (out string responseJson)
        {
            bool          successedOperation;
            TcpClient     connection = new TcpClient ();
            NetworkStream stream     = null;

            try
            {
                // Устанавливаем соединение для поиска дрона.
                connection.Connect (Const.NetworkConnection.DroneHostAddress, Const.NetworkConnection.Port.CToV.Discover);

                // Отправляем на дрон JSON-запрос и получаем ответ также в виде JSON.
                stream = connection.GetStream ();

                stream.WriteASCIIString (String.Format (Const.Discovering.CToV_Json,
                                                        Const.Discovering.ControllerTypeDefault, Const.Discovering.ControllerNameDefault,
                                                        Const.NetworkConnection.Port.VToC.Port, Const.NetworkConnection.Port.VToC.Stream, Const.NetworkConnection.Port.VToC.Control));

                while (stream.DataAvailable == false) ;

                responseJson = stream.ReadASCIIString (connection);

                // Проверяем значение поля status в пришедшем от дрона JSON.
                if ((int)JObject.Parse (responseJson).GetValue ("status") == Const.Discovering.VToC_ConnectionIsCorrect)
                {
                    successedOperation = true;
                }

                else successedOperation = false;
            }

            catch
            {
                responseJson = String.Empty;
                successedOperation = false;
            }

            finally
            {
                if (stream != null)
                {
                    stream.Close ();
                    stream.Dispose ();
                    connection.Close ();
                }

                connection.Dispose ();
            }

            return successedOperation;
        }

        public void TakeOff ()
        {
            PushCommand (TakeOffCommand.TakeOff);
        }

        public void Land ()
        {
            PushCommand (LandCommand.Land);
        }
    }
}