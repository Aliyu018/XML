using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<studentlist>" +
                          "<student>" +
                            "<stn>100</stn>" +
                            "<stname>Bill</stname>" +
                            "<stsurname>Gates</stsurname>" +
                          "</student>" +
                          "<student>" +
                            "<stn>200</stn>" +
                            "<stname>Elon</stname>" +
                            "<stsurname>Mask</stsurname>" +
                          "</student>" +
                        "</studentlist>");
            using (XmlTextWriter writer = new XmlTextWriter("d://students.xml", null))
            {
                writer.Formatting = Formatting.Indented;
                doc.Save(writer);
                MessageBox.Show("The XML file is Created");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "File Content";
            XElement List = XElement.Load("d://students.xml");
            MessageBox.Show(List.ToString());
            foreach (var node in List.Elements("student").Elements("stname"))
                label1.Text = label1.Text+" " +node.Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var doc = XDocument.Load("d://students.xml");
            //------------------------------------------------------
            XElement List = XElement.Load("d://students.xml");
            foreach (var node in List.Elements("student").Elements("stn"))
                if(textBox1.Text==node.Value)
                {
                    MessageBox.Show("Already Exists!");
                    return;
                }
            //-----------------------------------------------------
            XElement newStudent = new XElement
                ("student",new XElement("stn", textBox1.Text),
                           new XElement("stname", textBox2.Text),
                           new XElement("stsurname", textBox3.Text));
            doc.Element("studentlist").Add(newStudent);
            doc.Save("d://students.xml");
            MessageBox.Show("The new student has been successfully added");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var doc = XDocument.Load("d://students.xml");
            doc.Descendants("student")
                .Where(x => (string)x.Element("stn") == textBox4.Text)
                .Remove();
            doc.Save("d://students.xml");
            MessageBox.Show("The student is successfully deleted");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var doc = XDocument.Load("d://students.xml");
            var items = from item in doc.Descendants("student")
                        where item.Element("stn").Value == textBox7.Text
                        select item;
            foreach (XElement itemElement in items)
            {
                itemElement.SetElementValue("stname", textBox6.Text);
                itemElement.SetElementValue("stsurname",textBox5.Text);
            }
            doc.Save("d://students.xml");
        }
    }
}
