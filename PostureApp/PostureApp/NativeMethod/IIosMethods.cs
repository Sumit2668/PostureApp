namespace PostureApp.NativeMethod
{
    public interface IIosMethods
    {
        void ShowToast(string msg);
        void ShowLoader();
        void DismissLoader();
     }
}