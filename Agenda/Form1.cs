using Agenda.Properties;
using System;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Agenda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SqlConnection CriarConexao()
        {
            return new SqlConnection(Settings.Default.stringConexao);
        }

        private string comandoSql = string.Empty;

        private void Limpar()
        {
            txtNome.Clear();
            mskTelefone.Clear();
            txtEmail.Clear();
            txtRua.Clear();
            txtNum.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            mskCEP.Clear();
            txtUF.Clear();
            txtComplemento.Clear();
        }


        private void Form1_Load(object sender, EventArgs e)
        {   

            txt_Id.Focus();
            btnMenuF.Visible = false;
            picBook_F.Visible = false;
            
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            //txtNome.Focus();
            //txt_Id.Visible = false;

            SqlConnection sqlConexao = CriarConexao();

            if (txt_Id.Text != string.Empty)
            {
                MessageBox.Show("Contato já Cadastrado!");
                Limpar();
                txt_Id.Clear();
            }
            else
            {
                if (txtNome.Text == string.Empty)
                {
                    MessageBox.Show("Preencha os campos!");
                }
                else
                {
                    txt_Id.Enabled = false;
                    comandoSql = "insert into tblContatos(Nome, Telefone, Email, Rua, Num, Bairro, Cidade, CEP, UF, Complemento) Values(@Nome, @Telefone, @Email, @Rua, @Num, @Bairro, @Cidade, @CEP, @UF, @Complemento)";

                    SqlCommand comando = sqlConexao.CreateCommand();

                    comando.CommandText = comandoSql;

                    comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txtNome.Text;
                    comando.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = mskTelefone.Text;
                    comando.Parameters.Add("@Email", SqlDbType.VarChar).Value = txtEmail.Text;
                    comando.Parameters.Add("@Rua", SqlDbType.VarChar).Value = txtRua.Text;
                    comando.Parameters.Add("@Num", SqlDbType.VarChar).Value = txtNum.Text;
                    comando.Parameters.Add("@Bairro", SqlDbType.VarChar).Value = txtBairro.Text;
                    comando.Parameters.Add("@Cidade", SqlDbType.VarChar).Value = txtCidade.Text;
                    comando.Parameters.Add("@CEP", SqlDbType.VarChar).Value = mskCEP.Text;
                    comando.Parameters.Add("@UF", SqlDbType.VarChar).Value = txtUF.Text;
                    comando.Parameters.Add("@Complemento", SqlDbType.VarChar).Value = txtComplemento.Text;

                    try
                    {
                        sqlConexao.Open();
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Contato Cadastrado com sucesso!");
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        sqlConexao.Close();
                        Limpar();
                    }



                }
            }
           
        }
            

        private void btnConsultar_Click(object sender, EventArgs e)
        { 
            SqlConnection sqlConexao = CriarConexao();

            if (txt_Id.Text == string.Empty)
            {
                MessageBox.Show("Insira um Id Valido!");

            }


            else
            {
                comandoSql = " SELECT IdContato, Nome, Telefone, Email, Rua, Num, Bairro, Cidade, CEP, UF, Complemento FROM tblContatos  WHERE IdContato= " + txt_Id.Text;

                SqlCommand comando = sqlConexao.CreateCommand();
                comando.Parameters.Add("@IdContato", SqlDbType.Int).Value = Convert.ToString(txt_Id.Text);
                comando.CommandTimeout = 1800;

                comando.CommandText = comandoSql;

                SqlDataReader dataReader;
                sqlConexao.Open();

                try
                {
                    dataReader = comando.ExecuteReader();
                    if (dataReader.Read())
                    {
                        txt_Id.Text = dataReader[0].ToString();
                        txtNome.Text = dataReader[1].ToString();
                        mskTelefone.Text = dataReader[2].ToString();
                        txtEmail.Text = dataReader[3].ToString();
                        txtRua.Text = dataReader[4].ToString();
                        txtNum.Text = dataReader[5].ToString();
                        txtBairro.Text = dataReader[6].ToString();
                        txtCidade.Text = dataReader[7].ToString();
                        mskCEP.Text = dataReader[8].ToString();
                        txtUF.Text = dataReader[9].ToString();
                        txtComplemento.Text = dataReader[10].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao Consultar!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.ToString());
                }
                finally
                {
                    sqlConexao.Close();

                    //txt_Id.Clear();
                }
            }
            

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConexao = CriarConexao();

            comandoSql = "UPDATE tblContatos SET Nome = @Nome, Telefone = @Telefone, Email = @Email, Rua = @Rua, Num = @Num, Bairro = @Bairro, Cidade = @Cidade, CEP = @CEP, UF = @UF, Complemento = @Complemento WHERE IdContato=" + txt_Id.Text;
            SqlCommand comando = sqlConexao.CreateCommand();
            
            comando.Parameters.AddWithValue("@Id", txt_Id.Text);
            comando.Parameters.AddWithValue("@Nome", txtNome.Text);
            comando.Parameters.AddWithValue("@Telefone", mskTelefone.Text);
            comando.Parameters.AddWithValue("@Email", txtEmail.Text);
            comando.Parameters.AddWithValue("@Rua", txtRua.Text);
            comando.Parameters.AddWithValue("@Num", txtNum.Text);
            comando.Parameters.AddWithValue("@Bairro", txtBairro.Text);
            comando.Parameters.AddWithValue("@Cidade", txtCidade.Text);
            comando.Parameters.AddWithValue("@CEP", mskCEP.Text);
            comando.Parameters.AddWithValue("@UF", txtUF.Text);
            comando.Parameters.AddWithValue("@Complemento", txtComplemento.Text);

            comando.CommandTimeout = 1800;

            comando.CommandText = comandoSql;
            sqlConexao.Open();

            try
            {
                int i = comando.ExecuteNonQuery();
                if (i > 0)
                    MessageBox.Show("Cadastro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                sqlConexao.Close();
                Limpar();
            }

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConexao = CriarConexao();
            SqlCommand comando = sqlConexao.CreateCommand();
            if (MessageBox.Show("Deseja realmente excluir esse contato?", "Cuidado", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                MessageBox.Show("Operação cancela!");
            }
            else
            {
                comandoSql = "DELETE  FROM tblContatos WHERE IdContato = " + txt_Id.Text;
                comando.Parameters.Add("@IdContato", SqlDbType.Int).Value = Convert.ToString(txt_Id.Text);
                comando.CommandText = comandoSql;
            }
            try
            {
                sqlConexao.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Contato Excluido com sucesso!");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConexao.Close();
                Limpar();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void btnMenu_Click(object sender, EventArgs e)  //Fecha Menu 
        {
            int heig = 443, wid = 39;
            btnMenuF.Visible = true;
            panelMenu.Width = wid;
            panelMenu.Height = heig;
            btn_Minimiza.Location = new Point(606, 0);
            picBook_F.Visible = true;
        }

        private void btnMenuF_Click(object sender, EventArgs e) // Abri Menu
        {
            int heig = 443, wid = 133;
            btnMenuF.Visible = false;
            panelMenu.Width = wid;
            panelMenu.Height = heig;
            btn_Minimiza.Location = new Point(512, 0);
            picBook_F.Visible = false;

        }

        private void btn_Minimiza_Click(object sender, EventArgs e)
        {

            if (WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Minimized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Limpar();
            txt_Id.Clear();
        }
    }
    
}
