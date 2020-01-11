namespace Gravicar.ARDrone3.Communication
{
    public enum EFrameTargetBufferId : byte
    {
        CToV_Common = 10,

        CToV_RequiringForAcknowledgment = 11, 

        CToV_Emergency = 12, 

        CToV_VideoStream = 13,

        VToC_VideoStream = 125,

        VToC_RequiringForAcknowledgment = 126, 

        VToC_Common = 127
    }
}