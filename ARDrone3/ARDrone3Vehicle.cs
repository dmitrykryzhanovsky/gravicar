using System;
using System.Net.Sockets;

using Newtonsoft.Json.Linq;

using Horizon.Network;

using Gravicar.CommandModule;

namespace Gravicar.ARDrone3
{
    public abstract class ARDrone3Vehicle : AIVehicle
    {
        /// <summary>
        /// Поиск дрона и установка с ним первичного соединения.
        /// </summary>
        /// <param name="responseJson">JSON,возвращаемый дроном. Если при установке соединения или записи / чтении данных произошла 
        /// ошибка, <paramref name="responseJson"/> будет равен String.Empty. Подробнее о формате JSON’ов, пересылаемых при поиске 
        /// дрона и установке первичного соединения, см. ARSDK_Protocols.pdf, раздел 1.2.1.</param>
        /// <returns><list type="bullet">
        ///     <item>TRUE, если операция прошла успешно – контроллер и дрон обменялись JSON-ами с параметрами подключений.</item>
        ///     <item>FALSE, если операция завершилась неудачно. Возможные причины:<list type="bullet">
        ///         <item>не установлено соединение;</item>
        ///         <item>произошла ошибка во время передачи или приёма данных;</item>
        ///         <item>поле status в JSON-запросе, пришедшем от дрона, не равно <see cref="ARDrone3.ParamsAndSettings.Const.DiscoveringConnectionIsCorrect"/>.</item>
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
                responseJson       = String.Empty;
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
    }
}