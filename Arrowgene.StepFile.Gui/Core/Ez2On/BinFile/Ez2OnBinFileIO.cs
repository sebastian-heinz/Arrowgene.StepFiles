﻿using Arrowgene.Services.Buffers;
using Arrowgene.Services.Extra;

namespace Arrowgene.StepFile.Gui.Core.Ez2On.BinFile
{
    public class Ez2OnBinFileIO
    {
        public Ez2OnBinFile Read(string source)
        {
            byte[] dataBin = Utils.ReadFile(source);
            if (dataBin.Length < Ez2OnBinFile.HeaderSize)
            {
                return null;
            }
            IBuffer buffer = new StreamBuffer(dataBin);
            buffer.SetPositionStart();
            string header = buffer.ReadCString();
            Ez2OnBinFile file;
            switch (header)
            {
                case "M_CARD":
                    file = new Ez2OnCardBinFile();
                    break;
                case "M_GSTR":
                    file = new Ez2OnStrBinFile();
                    break;
                case "M_ID_FILTER":
                    file = new Ez2OnIdFilterBinFile();
                    break;
                case "M_ITEM":
                    file = new Ez2OnItemBinFile();
                    break;
                case "M_MUSIC":
                    file = new Ez2OnMusicBinFile();
                    break;
                case "M_QUEST":
                    file = new Ez2OnQuestBinFile();
                    break;
                case "M_RADIOMIX":
                    file = new Ez2OnRadiomixBinFile();
                    break;
                default: return null;
            }
            file.Read(buffer);
            return file;
        }

        public void Wrtire(string destination, Ez2OnBinFile binFile)
        {
            IBuffer buffer = new StreamBuffer();
            buffer.SetPositionStart();
            binFile.Write(buffer);
            Utils.WriteFile(buffer.GetAllBytes(), destination);
        }

    }
}
