using BankServer.Interfaces;
using BankSerializer;

using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BankServer.Generators
{
    public class NumberGenerator : IBaseGenerator
    {
        private ulong currentNumber = 0;
        private List<ulong> freeItems = new();
        private string path = "NumberGenerator.SaveData";
        public NumberGenerator()
        {
            LoadedData();
        }

        public void AddFreeItem(string item)
        {
            var numberString = item.Replace(" ", "");
            freeItems.Add(Convert.ToUInt64(numberString));
        }

        public string GetCurrentValue()
        {
            return string.Format("{0:0000 0000 0000 0000}", currentNumber);
        }

        public string GetNextValue()
        {
            if(freeItems.Count != 0)
            {
                var number = freeItems.First();
                freeItems.RemoveAt(0);

                return string.Format("{0:0000 0000 0000 0000}", number);
            }

            return string.Format("{0:0000 0000 0000 0000}", ++currentNumber);
        }

        ~NumberGenerator()
        {
            SaveData();
        }

        private void SaveData()
        {
            Serializer serializer = new Serializer();
            string data = $"{serializer.SerializeJSONList(freeItems)}CURRENTNUMBER{currentNumber}";

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.Write(data);
            }
        }

        private void LoadedData()
        {
            Serializer serializer = new Serializer();
            string? text = null;

            using (StreamReader reader = new StreamReader(path))
            {
                text = reader.ReadToEnd();
            }
            var data = text.Split("CURRENTNUMBER");



            freeItems = serializer.DeSerializeJSONList<List<ulong>>(data[0]) as List<ulong>;
            if(freeItems is null)
            {
                freeItems = new();
            }

            currentNumber = Convert.ToUInt64(data[1]);
        }
    }
}
