using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankClient.Bihaviors
{
    public class NumbersBihavior : Behavior<Entry>
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
            return (c >= '0' && c <= '9') || c ==' ';
        }
    }
}
