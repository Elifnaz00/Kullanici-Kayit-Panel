using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Personel_Kayıt
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-IDVTKKC\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

        void cmbDoldur()
        {
            baglanti.Open();

            SqlCommand doldur = new SqlCommand("SELECT sehir FROM iller", baglanti);
            SqlDataReader dr = doldur.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }


            baglanti.Close();
        }
        void Temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
            maskedTextBox1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'personelVeriTabaniDataSet1.Tbl_Personel' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet1.Tbl_Personel);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Personel (PerAd, PerSoyad, PerSehir, PerMaas, PerDurum, PerMeslek) values (@p1, @p2, @p3, @p4, @p5, @p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", textBox2.Text);
            komut.Parameters.AddWithValue("@p2", textBox3.Text);
            komut.Parameters.AddWithValue("@p3", comboBox1.Text);
            komut.Parameters.AddWithValue("@p4", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p5", label8.Text);
            komut.Parameters.AddWithValue("@p6", textBox4.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Eklendi.");
        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label8.Text = "True";

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label8.Text = "False";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            textBox1.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            maskedTextBox1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            textBox4.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            label8.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();

        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            if(label8.Text== "True")
            {
                radioButton1.Checked = true;
            }
            if(label8.Text== "False") 
            {
                radioButton2.Checked= true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbDoldur();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kayitsil = new SqlCommand("DELETE FROM Tbl_Personel WHERE Personelid= @k1 ", baglanti);
            kayitsil.Parameters.AddWithValue("@k1", textBox1.Text);

            kayitsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Silindi");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand guncelle = new SqlCommand("Update Tbl_Personel Set Perad= @a1, PerSoyad=@2, PerSehir=@a3, PerMaas=@a4, PerDurum=@a5, PerMeslek=@a6 WHERE Personelid=@a7 ", baglanti);
            guncelle.Parameters.AddWithValue("@a1", textBox2.Text);
            guncelle.Parameters.AddWithValue("@a2", textBox3.Text);
            guncelle.Parameters.AddWithValue("@a3", comboBox1.Text);
            guncelle.Parameters.AddWithValue("@a4", maskedTextBox1.Text);
            guncelle.Parameters.AddWithValue("@a5", label8.Text);
            guncelle.Parameters.AddWithValue("@a6", textBox4.Text);
            guncelle.Parameters.AddWithValue("@a7", textBox1.Text);
            guncelle.ExecuteNonQuery();
            baglanti.Close();
            
            MessageBox.Show("Güncellendi");
        }
    }
}
