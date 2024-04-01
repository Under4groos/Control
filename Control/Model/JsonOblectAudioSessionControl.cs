namespace Control.Model
{
    public class JsonOblectAudioSessionControl
    {
        public float Volume
        {
            get; set;
        } = 1;

        public string ProcessName
        {
            get; set;
        } = string.Empty;

        public int ProcessID
        {
            get; set;
        } = 0;


        public override string ToString()
        {
            return $"Name:{ProcessName} ID:{ProcessID} Volume:{Volume}";
        }
    }
}
