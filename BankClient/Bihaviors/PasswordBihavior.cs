namespace BankClient.Bihaviors
{
    public class PasswordBihavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            string newText = string.Empty;

            foreach (char c in e.NewTextValue)
            {
                if (IsValidAlphanumericCharacter(c))
                    newText += c;
            }

            entry.Text = newText;
        }

        private bool IsValidAlphanumericCharacter(char c)
        {
            return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9');
        }
    }
}
