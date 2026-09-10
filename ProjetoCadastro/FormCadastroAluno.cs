using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;

namespace ProjetoCadastro
{
    public partial class FormCadastroAluno : MaterialForm
    {
        string alunosFileName = "alunos.txt";
        bool isAlteracao = false;

        public FormCadastroAluno()
        {
            InitializeComponent();
        }

        private void FormCadastroAluno_Load(object sender, EventArgs e)
        {

        }

        private void FormCadastroAluno_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                e.Cancel = true;
            }
        }

        private void tabPageCadastro_Click(object sender, EventArgs e)
        {

        }

        private void txt_Click(object sender, EventArgs e)
        {

        }

        private void materialMaskedTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtSenha_Click(object sender, EventArgs e)
        {

        }

        
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidaFormulario()) //METODO DE VALIDAÇÃO
            {
                Salvar(); //MÉTODO PARA SALVAR EM ARQUIVO .TXT
                tabControlCadastro.SelectedIndex = 1; //MUDA PARA A SEGUNDA PÁGINA
            }
        }
        
        private bool ValidaFormulario() {

            var erro = string.Empty;

            if (string.IsNullOrEmpty(txtMatricula.Text))
                erro += "Matrícula obrigatória!\n";
            if (!DateTime.TryParse(txtDataNascimento.Text, out _))
                erro += "Data de Nascimento Inválida!\n";
            if (string.IsNullOrEmpty(txtEndereco.Text))
                erro += "Endereço obrigatória!\n";
            if (string.IsNullOrEmpty(txtBairro.Text))
                erro += "Bairro obrigatória!\n";
            if (string.IsNullOrEmpty(txtCidade.Text))
                erro += "Cidade obrigatória!\n";
            if (string.IsNullOrEmpty(txtSenha.Text))
                erro += "Senha obrigatória!\n";
            if (string.IsNullOrEmpty(erro))
                return true;
            else
            {
                MessageBox.Show(erro, "IFSP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void Salvar()
        {
            var recordLine = $"{txtMatricula.Text};" +
                             $"{txtNome.Text};" +
                             $"{txtDataNascimento.Text};" +
                             $"{txtEndereco.Text};" +
                             $"{txtBairro.Text};" +
                             $"{txtCidade.Text};" +
                             $"{txtEstado.Text};" +
                             $"{txtSenha.Text}";
            if (!isAlteracao)
            {
                var file = new StreamWriter(alunosFileName, true);
                file.WriteLine(recordLine);
                file.Close();
            }
            else
            {

            }
        }
    }
}