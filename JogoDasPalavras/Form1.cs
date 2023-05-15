using System.Globalization;
using System.Linq;

namespace JogoDasPalavras
{
    public partial class Form1 : Form
    {
        private Palavra palavra;

        public List<char> palpiteFinal = new List<char>();
        public Form1()
        {
            InitializeComponent();

            RegistrarEventos();

            palavra = new Palavra();

        }
        private void RegistrarEventos()
        {
            foreach (Button botao in pnlBotoes.Controls)
            {
                botao.Click += DarPalpite;
            }
        }
        public void DarPalpite(object sender, EventArgs e)
        {
            Button botaoClicado = (Button)sender;

            if (botaoClicado != buttonEnter)
            {
                char palpite = botaoClicado.Text[0];

                palpiteFinal.Add(palpite);
            }

            int i = 0;
            i = VerificarPalpite(i);
        }
        private int VerificarPalpite(int i)
        {
            if (palpiteFinal.Count == 5)
            {
                palavra.JogadorAcertou(palpiteFinal);

                palpiteFinal.ToArray();

                string palavraGerada = palavra.palavraSorteada;
                string palpiteJogador = String.Join("", palpiteFinal);

                foreach (var item in tblTentativas.Controls)
                {
                    if (item is RichTextBox richTextBox && richTextBox.Tag == palavra.Tentativas.ToString())
                    {
                        richTextBox.Text = palpiteFinal[i].ToString();

                        if (VerificaLetraSeIgualSemConsiderarAcento(palpiteFinal[i].ToString(), palavraGerada[i].ToString().ToUpper()))
                        {
                            richTextBox.BackColor = Color.Green;
                            richTextBox.Text = palavra.palavraSorteada[i].ToString().ToUpper();
                        }
                        else if (palavraGerada.ToString().ToUpper().Contains(palpiteFinal[i]))
                        {
                            richTextBox.BackColor = Color.Yellow;
                        }
                        else
                        {
                            richTextBox.BackColor = Color.Gray;
                        }
                        i++;
                    }
                }

                VerificarVitoria(palavraGerada, palpiteJogador);

                palpiteFinal.Clear();
            }
            return i;
        }
        private void VerificarVitoria(string palavraGerada, string palpiteJogador)
        {
            if (VerificaLetraSeIgualSemConsiderarAcento(palavraGerada.ToUpper(), palpiteJogador) == true)
            {
                MessageBox.Show("Você venceu! Parabéns!");
            }
            else if (palavra.Tentativas == 5)
            {
                MessageBox.Show("Você perdeu! A palavra era: " + palavraGerada);
            }
        }
        private bool VerificaLetraSeIgualSemConsiderarAcento(string letra1, string letra2)
        {
            return String.Compare(letra1, letra2, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0;
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void buttonEnter_Click(object sender, EventArgs e)
        {
            palpiteFinal.Clear();

            foreach (RichTextBox richTextBox in tblTentativas.Controls)
            {
                if (richTextBox.Tag.ToString() == palavra.Tentativas.ToString())
                {
                    palpiteFinal.Add(richTextBox.Text[0]);
                }

            }
        }
    }
}