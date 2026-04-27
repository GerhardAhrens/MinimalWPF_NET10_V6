namespace System.Windows
{
    internal static class AppMessage
    {
        public static MessageBoxResult AppExitMessage(string args = null)
        {
            MessageBoxResult result;

            try
            {
                string msgBoxTitle = LocalizationString.Get("MessageExit_Titel_DE");
                if (args != null)
                {
                    string msgBoxDescription = LocalizationString.Get("MessageExit_Text_DE", args);
                    result = MessageBox.Show(msgBoxDescription, msgBoxTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                }
                else
                {
                    string msgBoxDescription = LocalizationString.Get("MessageExit_Text_DE");
                    result = MessageBox.Show(msgBoxDescription, msgBoxTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                }

                return result;
            }
            catch (InvalidOperationException ex)
            {
                string errorText = ex.Message;
                throw;
            }
        }
    }
}
