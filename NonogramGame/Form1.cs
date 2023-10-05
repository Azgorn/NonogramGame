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
                if(result.Length == 0)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            Dictionary<int, List<int>> rowSequence = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> colSequence = new Dictionary<int, List<int>>();
            byte[,] solution = new byte[5, 5];
            solution = GenerateSolution();
            rowSequence = GenerateHintsRow(solution);
            colSequence = GenerateHintsCol(solution);
            PrintHintsRow(rowSequence);
            PrintsHintsCol(colSequence);
        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}