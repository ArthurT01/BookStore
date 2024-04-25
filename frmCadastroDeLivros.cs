using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookStore
{
    public partial class frmCadastroDeLivros : Form
    {
        MySqlConnection conexao;
        MySqlCommand comando;
        MySqlDataReader dr;
        string strSQL;

        public frmCadastroDeLivros()
        {
            InitializeComponent();
        }

        private void btnSalvarCadastro_Click(object sender, EventArgs e)
        {
            conexao = new MySqlConnection("Server = localhost; Database = bookstore; Uid = senai; Pwd = 1234 ");
            strSQL = "INSERT INTO t_Livros (titulo ,autor ,ano) VALUES (@titulo, @autor, @ano)";
            comando = new MySqlCommand(strSQL, conexao);

            try
            {
                comando.Parameters.AddWithValue("@titulo", txtTitulo.Text);
                comando.Parameters.AddWithValue("@autor", txtAutor.Text);
                comando.Parameters.AddWithValue("@ano", txtAno.Text);

                conexao.Open();

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO: " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
            MessageBox.Show("Cadastro feito!");
        }

        private void btnBuscarLivro_Click(object sender, EventArgs e)
        {

            conexao = new MySqlConnection("Server = localhost; Database = bookstore; Uid = senai; Pwd = 1234 ");
            strSQL = "SELECT * FROM t_Livros WHERE codigo = @codigo";
            comando = new MySqlCommand(strSQL, conexao);

            comando.Parameters.AddWithValue("@codigo", txtPesquisa.Text);

            conexao.Open();

            dr = comando.ExecuteReader();


            if(dr.HasRows)
            {

                while (dr.Read())
                {
                    limpa();
                    txtTitulo.Text = Convert.ToString(dr["titulo"]);
                    txtAutor.Text = Convert.ToString(dr["autor"]);
                    txtAno.Text = Convert.ToString(dr["ano"]);
                }
            }
            else
            {
                MessageBox.Show("Livro não encontrado!");
            }
            conexao.Close();
        }

        

        private void block()//DEIXA OS CAMPOS BLOQUEADOS PARA O PREENCHIMENTO
        {
            txtTitulo.ReadOnly = true;
            txtAutor.ReadOnly = true;
            txtAno.ReadOnly = true;

            limpa();
           
        }   

        private void limpa()//LIMPA TODOS OS CAMPOS
        {
            txtTitulo.Text = "";
            txtAutor.Text = "";
            txtAno.Text = "";
            
        }

        private void frmCadastroDeLivros_Load(object sender, EventArgs e)
        {
            var tipo = (Owner as frmLogin)?.tipo;

            if(tipo == "A")
            {
                txtTitulo.ReadOnly = false;
                txtAutor.ReadOnly = false;
                txtAno.ReadOnly = false;
            }
            else
            {
                block();
            }
        }

    }
}
