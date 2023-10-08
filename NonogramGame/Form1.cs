using System.Runtime.Versioning;
using System.Web;

namespace NonogramGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private byte[,] GenerateSolution()
        {
            Random random = new Random();

            byte[,] solution = new byte[5, 5];
            for (int n = 0; n < 5; n++)
            {
                for (int i = 0; i < 5; i++)
                {
                    solution[n, i] = (byte)random.Next(2);
                }
            }
            return solution;
        }
        private Dictionary<int, List<int>> GenerateHintsRow(byte[,] solution)
        {
            Dictionary<int, List<int>> rowSequence = new Dictionary<int, List<int>>();
            for (int i = 0; i < Math.Sqrt(solution.Length); i++)
            {
                int count = 0;
                List<int> seq = new List<int>();
                for (int j = 0; j < Math.Sqrt(solution.Length); j++)
                {
                    if (solution[i, j] != 0)
                    {
                        count++;
                    }
                    else if ((count != 0 && solution[i, j] == 0))
                    {
                        seq.Add(count);
                        count = 0;
                    }
                    if (j == Math.Sqrt(solution.Length) - 1 && count != 0)
                    {
                        seq.Add(count);
                    }
                }
                rowSequence.Add(i, seq);
            }
            return rowSequence;
        }
        private Dictionary<int, List<int>> GenerateHintsCol(byte[,] solution)
        {
            Dictionary<int, List<int>> colSequence = new Dictionary<int, List<int>>();
            for (int i = 0; i < Math.Sqrt(solution.Length); i++)
            {
                int count = 0;
                List<int> seq = new List<int>();
                for (int j = 0; j < Math.Sqrt(solution.Length); j++)
                {
                    if (solution[j, i] != 0)
                    {
                        count++;
                    }
                    else if ((count != 0 && solution[j, i] == 0))
                    {
                        seq.Add(count);
                        count = 0;
                    }
                    if (j == Math.Sqrt(solution.Length) - 1 && count != 0)
                    {
                        seq.Add(count);
                    }
                }
                colSequence.Add(i, seq);
            }
            return colSequence;
        }
        private void PrintHintsRow(Dictionary<int, List<int>> rowSequence)
        {
            int i = 0;

            foreach (var kvp in rowSequence)
            {
                Label label = this.Controls.Find("label" + (i + 6).ToString(), true).FirstOrDefault() as Label;
                List<string> list = new List<string>();

                foreach (var item in kvp.Value)
                {
                    list.Add(item.ToString());
                }

                var result = String.Join(", ", list);
                label.Text = result;
                if (result.Length == 0)
                {
                    label.Text = "0";
                }

                i++;

                if (i > 5)
                {
                    break;
                }
            }
        }
        private void PrintsHintsCol(Dictionary<int, List<int>> colSequence)
        {
            int i = 1;

            foreach (var kvp in colSequence)
            {
                Label label = this.Controls.Find("label" + i.ToString(), true).FirstOrDefault() as Label;
                List<string> list = new List<string>();

                foreach (var item in kvp.Value)
                {
                    list.Add(item.ToString());
                }

                var result = String.Join(", ", list);
                label.Text = result;
                if (result.Length == 0)
                {
                    label.Text = "0";
                }

                i++;

                if (i > 6)
                {
                    break;
                }
            }
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            Panel panel = (Panel)sender; // Get the panel that triggered the event

            if (e.Button == MouseButtons.Left)
            {
                // Left-clicked, change the panel's background color to black
                panel.BackColor = Color.FromArgb(255, 76, 76, 76);
                panel.BackgroundImage = null;
            }
            else if (e.Button == MouseButtons.Right)
            {
                // Right-clicked, change the panel's background color to red
                panel.BackgroundImage = Properties.Resources.BlackX;
                panel.BackColor = Color.White;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                // Middle-clicked (scroll wheel click), change the panel's background color to black
                panel.BackColor = Color.White;
                panel.BackgroundImage = null;
            }
        }

        static byte[] To1DArray(byte[,] input)
        {
            // Step 1: get total size of 2D array, and allocate 1D array.
            int size = input.Length;
            byte[] result = new byte[size];

            // Step 2: copy 2D array elements into a 1D array.
            int write = 0;
            for (int i = 0; i <= input.GetUpperBound(0); i++)
            {
                for (int z = 0; z <= input.GetUpperBound(1); z++)
                {
                    result[write++] = input[i, z];
                }
            }
            // Step 3: return the new array.
            return result;
        }
        private Boolean IsCorrect(byte[] userInput, byte[,] answer)
        {
            userInput = new byte[25];
            bool isCorrect = false;
            int indexPanel = 1;
            int count = 0;
            byte[] flatAnswer = To1DArray(answer);



            while (true)
            {
                Panel panel = this.Controls.Find("panel" + indexPanel.ToString(), true).FirstOrDefault() as Panel;
                Color panelColor = panel.BackColor;
                if (panel.BackgroundImage == null && (panelColor == Color.FromArgb(255, 76, 76, 76)))
                {
                    userInput[count] = 1;
                }
                else if (panel.BackgroundImage != null)
                {
                    userInput[count] = 0;
                }
                indexPanel++;
                count++;

                if (count >= 25)
                {
                    break;
                }
            }
            for (int j = 0; j < flatAnswer.Length; j++)
            {
                if (flatAnswer[j] == userInput[j])
                {
                    isCorrect = true;
                }
                else
                    return false;
            }
            return isCorrect;
        }
        Random random = new Random();
        byte[,] solution = {
            {1, 0, 1, 1, 0},
            {0, 0, 0, 1, 0},
            {1, 1, 0, 0, 0},
            {0, 1, 0, 1, 1},
            {1, 0, 0, 0, 0 },
        };

        private void Form1_Load(object sender, EventArgs e)
        {
            Dictionary<int, List<int>> rowSequence = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> colSequence = new Dictionary<int, List<int>>();
            rowSequence = GenerateHintsRow(solution);
            colSequence = GenerateHintsCol(solution);
            PrintHintsRow(rowSequence);
            PrintsHintsCol(colSequence);
        }

        private void btnCheckSolution_Click(object sender, EventArgs e)
        {
            byte[] userInput = new byte[25];
            if (IsCorrect(userInput, solution))
            {
                MessageBox.Show("Right answer");
            }
            else
                MessageBox.Show("Wrong answer");
        }
    }
}