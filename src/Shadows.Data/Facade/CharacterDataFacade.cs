using Shadows.Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Shadows.Data.Facade
{
    public class CharacterDataFacade
    {
        public List<CharacterData> GetList()
        {
            DirectoryInfo charDir = AppFiles.GetCharactersDir();

            List<CharacterData> list = new List<CharacterData>();

            FileInfo[] files = charDir.GetFiles(String.Format("*.{0}", CharacterData.FILE_EXTENSION));

            foreach (FileInfo fi in files)
            {
                TextReader reader = new StreamReader(fi.FullName);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(CharacterData));
                object? data = xmlSerializer.Deserialize(reader);
                reader.Close();

                if (data != null)
                {
                    CharacterData ch = (CharacterData)data;
                    list.Add(ch);
                }
            }

            if (list.Count == 0)
            {
                list.Add(CharacterData.GetDemo());
            }

            list.Add(new CharacterData(CharacterData.NEW_LABEL));

            return list;
        }

        private string GetFilePath(CharacterData character)
        {
            DirectoryInfo diChars = AppFiles.GetCharactersDir();
            string filePath = Path.Combine(diChars.FullName, character.FileName);
            return filePath;
        }

        public void Delete(CharacterData character)
        {
            FileInfo fi = new FileInfo(GetFilePath(character));
            if (fi.Exists)
            {
                fi.Delete();
            }
        }

        public void Save(CharacterData character)
        {
            if (character == null) return;

            character.GenerateFileName();

            XmlSerializer xmlSerializer = new XmlSerializer(character.GetType());

            TextWriter textWriter = new StreamWriter(GetFilePath(character));
            xmlSerializer.Serialize(textWriter, character);
            textWriter.Close();
        }
    }
}
