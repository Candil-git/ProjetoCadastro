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
        int indexSelecionado = 0;


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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidaFormulario()) //METODO DE VALIDAÇÃO
            {
                Salvar(); //MÉTODO PARA SALVAR EM ARQUIVO .TXT
                tabControlCadastro.SelectedIndex = 1; //MUDA PARA A SEGUNDA PÁGINA
            }
        }

        private void livAlunos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Editar();
        }

        private void tabPageConsulta_Enter(object sender, EventArgs e)
        {
            CarregaListView();
        }

        private bool ValidaFormulario()
        {

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
                             $"{txtDataNascimento.Text};" +
                             $"{txtNome.Text};" +
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
                var alunos = File.ReadAllLines(alunosFileName);
                alunos[indexSelecionado] = recordLine;
                File.WriteAllLines(alunosFileName, alunos);
            }
        }

        private void CarregaListView()
        {
            Cursor.Current = Cursors.WaitCursor;

            livAlunos.View = View.Details;
            livAlunos.FullRowSelect = true;
            livAlunos.Items.Clear();
            livAlunos.Columns.Clear();

            livAlunos.Columns.Add("Prontuário");
            livAlunos.Columns.Add("Dt.Nasc");
            livAlunos.Columns.Add("Nome");
            livAlunos.Columns.Add("Endereço");
            livAlunos.Columns.Add("Bairro");
            livAlunos.Columns.Add("Cidade");
            livAlunos.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            var alunos = File.ReadAllLines(alunosFileName);

            foreach (var line in alunos)
            {
                var campos = line.Split(';');
                livAlunos.Items.Add(new ListViewItem(campos));

            }
            ;
            livAlunos.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            Cursor.Current = Cursors.Default;

        }


        private void Editar()
        {
            if (livAlunos.SelectedIndices.Count > 0)
            {
                indexSelecionado = livAlunos.SelectedItems[0].Index;
                isAlteracao = true;

                var item = livAlunos.SelectedItems[0];

                txtMatricula.Text = item.SubItems[0].Text;
                txtDataNascimento.Text = item.SubItems[1].Text;
                txtNome.Text = item.SubItems[2].Text;
                txtEndereco.Text = item.SubItems[3].Text;
                txtBairro.Text = item.SubItems[4].Text;
                txtCidade.Text = item.SubItems[5].Text;
                txtEstado.Text = item.SubItems[6].Text;
                txtSenha.Text = item.SubItems[7].Text;
                tabControlCadastro.SelectedIndex = 0;
                txtMatricula.Focus();
            }
            else
            {
                MessageBox.Show("Selecione algun aluno", "Atenção!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

      
    }
}