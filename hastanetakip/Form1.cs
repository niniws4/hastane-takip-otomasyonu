using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace sinav
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string dizinyolu = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
        private void button1_Click(object sender, EventArgs e)
        {

            MySqlConnection baglan = new MySqlConnection();
            baglan.ConnectionString = "server=localhost;user=root;password='';database=hastane";
            baglan.Open();

            string yol = openFileDialog1.FileName.ToString();
            string resimadi = System.IO.Path.GetFileName(openFileDialog1.FileName);
            string resimyolu = "\\resimler\\" + resimadi;

            try
            {
                
                MySqlCommand kaydet = new MySqlCommand();
                kaydet.CommandText = "insert into doktorlar values(@1,@2,@3,@4)";
                kaydet.Connection = baglan;
                kaydet.Parameters.Add("@1",int.Parse(textBox1.Text));
                kaydet.Parameters.Add("@2",textBox2.Text);
                kaydet.Parameters.Add("@3",comboBox1.Text);
                kaydet.Parameters.Add("@4",resimyolu);
                if (kaydet.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("eklendi.");
                }
                else
                {
                    MessageBox.Show("eklenemedi");
                }
            }
            catch
            {

                MessageBox.Show("hata");
            }

            baglan.Close();


        }
        bool restbutton = false;
        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Resim yükle";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == "")
            {
                MessageBox.Show("Resim seçmediniz");
            }
            else
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
            }
            restbutton = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MySqlConnection baglan = new MySqlConnection();
            baglan.ConnectionString = "server=localhost;user=root;password='';database=hastane";
            baglan.Open();
            MySqlCommand getir = new MySqlCommand();
            getir.CommandText = "select distinct(poliklinik) from doktorlar";
            getir.Connection = baglan;

            MySqlDataReader oku = getir.ExecuteReader();
            if (oku.HasRows)
            {
                while (oku.Read())
                {
                    
                    comboBox2.Items.Add(oku[0].ToString());
                }
            }
            else
            {
                MessageBox.Show("bulunamadı");
            }
           
            


            baglan.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlConnection baglan = new MySqlConnection();
            baglan.ConnectionString = "server=localhost;user=root;password='';database=hastane";
            baglan.Open();
            MySqlCommand kaydet = new MySqlCommand();
            kaydet.CommandText = "insert into randevular values(@1,@2,@3,@4,@5,@6,@7)";
            kaydet.Connection = baglan;
            kaydet.Parameters.Add("@1",int.Parse(textBox3.Text));
            kaydet.Parameters.Add("@2", textBox4.Text);
            kaydet.Parameters.Add("@3", textBox5.Text);
            kaydet.Parameters.Add("@4", comboBox2.Text);
            kaydet.Parameters.Add("@5", comboBox3.Text);
            kaydet.Parameters.Add("@6", Convert.ToDateTime(dateTimePicker1.Text));
            kaydet.Parameters.Add("@7", textBox6.Text);

            if (kaydet.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("kaydedildi");
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                
            }
            else
            {
                MessageBox.Show("hata");
            }

            baglan.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MySqlConnection  baglan = new MySqlConnection();
            baglan.ConnectionString = "server=localhost;user=root;password='';database=hastane";
            baglan.Open();

            MySqlCommand ara = new MySqlCommand();
            ara.CommandText = "select * from doktorlar where doktoradi=@1";
            ara.Connection = baglan;
            ara.Parameters.Add("@1",textBox7.Text);
            MySqlDataReader oku = ara.ExecuteReader();
            if (oku.HasRows)
            {
                while(oku.Read())
                {
                    textBox9.Text = oku[0].ToString();
                    textBox8.Text = oku[1].ToString();
                    textBox10.Text=oku[2].ToString();
                    pictureBox2.ImageLocation = "C:\\resimler\\doktor.jpg";
                }            
            }
            else
            {
                MessageBox.Show("aranan doktor yok");
            }

            baglan.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MySqlConnection baglan = new MySqlConnection();
            baglan.ConnectionString = "server=localhost;user=root;password='';database=hastane";
            baglan.Open();

            MySqlCommand listele = new MySqlCommand();
            listele.CommandText = "select * from randevular where doktor=@1";
            listele.Connection = baglan;
            listele.Parameters.Add("@1",textBox7.Text);

            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter(listele);
            adp.Fill(ds,"randevular");
            dataGridView1.DataSource = ds.Tables["randevular"];

            baglan.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            MySqlConnection baglan = new MySqlConnection();
            baglan.ConnectionString = "server=localhost;user=root;password='';database=hastane";
            baglan.Open();
            MySqlCommand getir = new MySqlCommand();
            getir.CommandText = "select doktoradi from doktorlar where poliklinik=@1";
            getir.Connection = baglan;
            getir.Parameters.Add("@1",comboBox2.Text);
            comboBox3.Items.Clear();
            MySqlDataReader oku = getir.ExecuteReader();
            if (oku.HasRows)
            {
                while (oku.Read())
                {

                    comboBox3.Items.Add(oku[0].ToString());
                }
            }
            else
            {
                comboBox3.Items.Clear();
                MessageBox.Show("bulunamadı");
            }


            baglan.Close();
        }
    }
}
