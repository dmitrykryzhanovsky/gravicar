using Gravicar.ARDrone3.Network;

namespace Gravicar.ARDrone3.Commands
{
    /// <summary>
    /// Базовый класс для команды ARDrone 3.
    /// </summary>
    internal abstract class ARDrone3Command : NetworkFrame, ICommand
    {
        protected ARDrone3Command (ECommandProject commandProject, ECommandClass commandClass, ECommandId commandId, int commandParametersSegmentLength) : 
            base (EFrameDataType.Common, EFrameTargetBufferId.CToV_Common, Const.Command.FrameAndCommandHeaderLength + commandParametersSegmentLength)
        {
            FrameKit.EncodeCommandHeaderTo (_encoded, Const.Frame.HeaderLength, commandProject, commandClass, commandId);
        }

        public byte [] Encode ()
        {
            EncodeCommandParameters (Const.Command.FrameAndCommandHeaderLength);

            return _encoded;
        }        

        protected virtual void EncodeCommandParameters (int index)
        {
        }
    }
}