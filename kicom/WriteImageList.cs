﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace kicom {
    public class WriteImageList {
        private static WriteImageList UniqueInstance = new WriteImageList();
        private static Queue<string> ListingQueue = new Queue<string>();

        private WriteImageList() {
            FileInfo _fileinfo = new FileInfo("ImageData.xml");

            if (!_fileinfo.Exists) {
                using (XmlTextWriter writer = new XmlTextWriter("ImageData.xml", System.Text.Encoding.UTF8)) {
                    writer.WriteStartDocument();
                    writer.Formatting = Formatting.Indented;
                    //writer.Indentation = 2;
                    writer.WriteStartElement("Images");
                    writer.WriteEndElement();
                    writer.Close();
                }
            }

            //addImageListTask();

        }

        public static WriteImageList GetInstance() {
            return UniqueInstance;
        }

        private async Task addImageListTask() {
            while (true) {
                //var result = await OverWriting();
                //await Task.Delay(1000);
            }
        }

        public bool pushList(string _data) {
            if (ListingQueue != null) {
                ListingQueue.Enqueue(_data);
                OverWriting();
                return true;
            }
            return false;
        }

        private bool OverWriting() {
        //private async Task<bool> OverWriting() {
            if (ListingQueue.Any()) {
                XmlDocument xmlDoc;
                XmlElement xmlEle;
                XmlNode newNode;

                xmlDoc = new XmlDocument();
                xmlDoc.Load("ImageData.xml"); // XML문서 로딩

                newNode = xmlDoc.SelectSingleNode("Images"); // 추가할 부모 Node 찾기
                xmlEle = xmlDoc.CreateElement("Image");
                newNode.AppendChild(xmlEle);
                newNode = newNode.LastChild;
                xmlEle = xmlDoc.CreateElement("ImagePath"); // 추가할 Node 생성
                xmlEle.InnerText = ListingQueue.Peek();
                ListingQueue.Dequeue();
                newNode.AppendChild(xmlEle); // 위에서 찾은 부모 Node에 자식 노드로 추가..

                xmlDoc.Save("ImageData.xml"); // XML문서 저장..
                xmlDoc = null;

                return true;
            }
            return false;
        }
    }
}
