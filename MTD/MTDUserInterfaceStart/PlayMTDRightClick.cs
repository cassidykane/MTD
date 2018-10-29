using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTDClasses;

namespace MTDUserInterface
{
    public partial class PlayMTDRightClick : Form
    {
        #region Bugs and Questions
        // computer plays on seemingly random mexican train PBs
        // computer's train sometimes stays open when it passes
        // ComputerPlayOnTrain(): how do i use the "train" param?
        // throws an argument out of range exception in the hand index when i try to play on a train
        #endregion

        #region instance_variables
        BoneYard boneYard;
        MexicanTrain mexicanTrain;
        PlayerTrain computerTrain;
        PlayerTrain userTrain;
        Hand computerHand;
        Hand userHand;
        List<PictureBox> userHandPBs;
        List<PictureBox> mexicanTrainPBs;
        List<PictureBox> userTrainPBs;
        List<PictureBox> computerTrainPBs;
        Domino userDominoInPlay = null;
        int indexOfDominoInPlay;
        int nextDrawIndex = 10;
        int nextUserTrainPBIndex = 0;
        int nextMexicanTrainPBIndex = 0;
        int nextComputerTrainPBIndex = 0;
        int engineValue;
        string currentTurn;
        #endregion

        #region Methods

        // loads the image of a domino into a picture box
        // verify that the path for the domino files is correct
        private void LoadDomino(PictureBox pb, Domino d)
        {
            pb.Image = Image.FromFile(System.Environment.CurrentDirectory
                        + "\\..\\..\\Dominos\\" + d.Filename);
        }

        // loads all of the dominos in a hand into a list of pictureboxes
        private void LoadHand(List<PictureBox> pbs, Hand h)
        {
            for (int i = 0; i < pbs.Count; i++)
            {
                PictureBox pb = pbs[i];
                Domino d = h[i];
                LoadDomino(pb, d);
            }
        }

        // dynamically creates the "next" picture box for the user's hand
        // the instance variable nextDrawIndex should be passed as a parameter
        // if you change the layout of the form, you'll have to change the math here
        private PictureBox CreateUserHandPB(int index) 
        {
            PictureBox pb = new PictureBox();
            pb.Visible = true;
            pb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pb.Location = new System.Drawing.Point(24 + (index % 5) * 110, 366 + (index / 5) * 60);
            pb.Size = new System.Drawing.Size(100, 50);
            pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Controls.Add(pb);
            return pb;
        }

        // adds the mouse down event handler to a picture box
        private void EnableHandPB(PictureBox pb)
        {
            pb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.handPB_MouseDown);
        }

        // adds the mouse down event handler to all of the picture boxes in the users hand pb list
        private void EnableUserHandPBs()
        {
            for (int i = 0; i < userHandPBs.Count; i++)
            {
                PictureBox pb = userHandPBs[i];
                EnableHandPB(pb);
            }
        }

        // removes the mouse down event handler from a picture box
        private void DisableHandPB(PictureBox pb)
        {
            pb.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.handPB_MouseDown);
        }

        // removes all of the mouse down event handlers from the picture boxes in the users hand pb list
        private void DisableUserHandPBs()
        {
            for (int i = 0; i < userHandPBs.Count; i++)
            {
                PictureBox pb = userHandPBs[i];
                DisableHandPB(pb);
            }
        }

        // unloads the domino image from a picture box in a train
        public void RemoveDominoFromPB(int index, List<PictureBox> pBs)
        {
            PictureBox pB = pBs[index];
            pB.Image = null;
        }
		
		// removes a picture box from the form dynamically
		public void RemovePBFromForm(PictureBox pb)
		{
			this.Controls.Remove(pb);
            pb = null;
		}

        public void UserPlayOnTrain(Domino d, Train train, List<PictureBox> trainPBs)
        {
            // plays a domino on a train.  Loads the appropriate train pb, 
            userHand.Play(d, train);
            LoadDomino(trainPBs[train.Count], d);

            // removes the domino pb from the hand, updates the train status label ,
            RemovePBFromForm(userHandPBs[indexOfDominoInPlay]);
            nextDrawIndex--;
            if (train is PlayerTrain && train.Equals(userTrain))
                (train as PlayerTrain).Close();
            UpdateUI();

            // disables the hand pbs and disables appropriate buttons
            DisableUserMove();
        }

        public void ComputerPlayOnTrain(Domino d, Train train, List<PictureBox> trainPBs, int pbIndex)
        {
            // adds a domino picture to a train
            LoadDomino(trainPBs[pbIndex], d);
        }

        public bool MakeComputerMove(bool canDraw)
        {
            // calls play for on the computer's hand for each train, gets the next pb index and 
            // plays the domino on the train.  
            // BECAUSE play throws an exception, tries to first play on one train and
            // in the catch block tries the next and so on
            // when the computer can not play on any train, the computer draws and returns false.
            // if the method is called with canDraw = false, this last step is omitted and the method
            // returns false

            try
            {
                Domino d = computerHand.Play(userTrain);
                ComputerPlayOnTrain(d, userTrain, userTrainPBs, nextUserTrainPBIndex);
                nextUserTrainPBIndex++;
            }
            catch
            {
                try
                {
                    Domino d = computerHand.Play(mexicanTrain);
                    ComputerPlayOnTrain(d, mexicanTrain, mexicanTrainPBs, nextMexicanTrainPBIndex);
                    nextMexicanTrainPBIndex++;
                }
                catch
                {
                    try
                    {
                        Domino d = computerHand.Play(computerTrain);
                        int nextPBIndex = computerTrain.Count;
                        ComputerPlayOnTrain(d, computerTrain, computerTrainPBs, nextComputerTrainPBIndex);
                        nextComputerTrainPBIndex++;
                    }
                    catch
                    {
                        if (canDraw)
                            computerHand.Draw(boneYard);
                        return false;
                    }
                }
            }
            return true;
        }

        public void CompleteComputerMove()
        {
            // update labels on the UI and disable the users hand
            UpdateUI();
            DisableUserMove();

            // call MakeComputerMove (maybe twice)
            // update the labels on the UI
            bool played = MakeComputerMove(true);
            if (!played)
                if (!MakeComputerMove(false))
                    computerTrain.Open();
            UpdateUI();

            // determine if the computer won or if it's now the user's turn
            if (computerHand.IsEmpty)
                MessageBox.Show("Computer wins");
            else
            {
                currentTurn = "user";
                EnableUserMove();
            }
            
        }

        public void EnableUserMove()
        {
            // enable the hand pbs, buttons and update labels on the UI

            EnableUserHandPBs();
            drawButton.Enabled = true;
            passButton.Enabled = true;
            UpdateUI();
        }

        public void DisableUserMove()
        {
            DisableUserHandPBs();
            drawButton.Enabled = false;
            passButton.Enabled = false;
        }

        public void UpdateUI()
        {
            userTrainStatusLabel.Text = userTrain.IsOpen ? "Open" : "Closed";
            computerTrainStatusLabel.Text = computerTrain.IsOpen ? "Open" : "Closed";
            LoadHand(userHandPBs, userHand);
        }
        
        public void SetUp()
        {
            // instantiate boneyard and hands
            boneYard = new BoneYard(9);
            userHand = new Hand(boneYard, 2);
            computerHand = new Hand(boneYard, 2);
            userHandPBs = new List<PictureBox>();
            mexicanTrainPBs = new List<PictureBox>();
            userTrainPBs = new List<PictureBox>();
            computerTrainPBs = new List<PictureBox>();

            // find the highest double in each hand
            Domino highestUserDouble = (userHand.IndexOfHighDouble() >= 0) ? userHand[userHand.IndexOfHighDouble()] : null;
            Domino highestComputerDouble = (computerHand.IndexOfHighDouble() >= 0) ? computerHand[computerHand.IndexOfHighDouble()] : null;

            // determine who should go first, remove the highest double from the appropriate hand
            // and display the highest double in the UI
            // instantiate trains now that you know the engine value
            if (highestUserDouble == null && highestComputerDouble == null)
            {
                Domino d = boneYard.Draw();
                while (!d.IsDouble())
                {
                    boneYard.Add(d);
                    d = boneYard.Draw();
                }
                LoadDomino(enginePB, d);
                currentTurn = "user";
            }
            else if (highestComputerDouble == null || 
                     highestUserDouble.Score > highestComputerDouble.Score)
            {
                userHand.RemoveAt(userHand.IndexOfHighDouble());
                LoadDomino(enginePB, highestUserDouble);
                engineValue = highestUserDouble.Side1;
                currentTurn = "user";
                nextDrawIndex--;
            }
            else
            {
                computerHand.RemoveAt(computerHand.IndexOfHighDouble());
                LoadDomino(enginePB, highestComputerDouble);
                engineValue = highestComputerDouble.Side1;
                currentTurn = "computer";
            }
            mexicanTrain = new MexicanTrain(engineValue);
            userTrain = new PlayerTrain(userHand, engineValue);
            computerTrain = new PlayerTrain(computerHand, engineValue);

            // create all of the picture boxes for the user's hand and load the dominos for the hand
            for (int i = 0; i < nextDrawIndex; i++)
            {
                userHandPBs.Add(CreateUserHandPB(i));
            }
            LoadHand(userHandPBs, userHand);

            // Add the picture boxes for each train to the appropriate list of picture boxes
            foreach (PictureBox mexicanTrainPB in tableLayoutPanel2.Controls)
                mexicanTrainPBs.Add(mexicanTrainPB);
            foreach (PictureBox userTrainPB in tableLayoutPanel1.Controls)
                userTrainPBs.Add(userTrainPB);
            foreach (PictureBox computerTrainPB in tableLayoutPanel3.Controls)
                computerTrainPBs.Add(computerTrainPB);

            // update the labels on the UI
            // if it's the computer's turn, let her play
            // if it's the user's turn, enable the pbs so she can play
            UpdateUI();
            if (currentTurn == "user")
                EnableUserMove();
            else
                CompleteComputerMove();
        }

        // remove all of the domino pictures for the user's hand
        // reset the nextDrawIndex to 10
        public void TearDown()
        {
            // remove all of the domino pictures for each train
            foreach (PictureBox pb in mexicanTrainPBs)
                RemoveDominoFromPB(mexicanTrainPBs.IndexOf(pb), mexicanTrainPBs);
            foreach (PictureBox pb in computerTrainPBs)
                RemoveDominoFromPB(computerTrainPBs.IndexOf(pb), computerTrainPBs);
            foreach (PictureBox pb in userTrainPBs)
                RemoveDominoFromPB(userTrainPBs.IndexOf(pb), userTrainPBs);

            // remove all of the domino pictures for the user's hand
            // reset the nextDrawIndex to 10
            foreach (PictureBox pb in userHandPBs)
                RemovePBFromForm(pb);
            nextDrawIndex = 10;
        }
        #endregion

        public PlayMTDRightClick()
        {
            InitializeComponent();
            //SetUp();
        }

        // when the user right clicks on a domino, a context sensitive menu appears that
        // let's the user know which train is playable.  Green means playable.  Red means not playable.
        // the event handler for the menu item is enabled and disabled appropriately.
        private void whichTrainMenu_Opening(object sender, CancelEventArgs e)
        {
			
            bool? mustFlip = false;
            if (userDominoInPlay != null)
            {
                mexicanTrainItem.Click -= new System.EventHandler(this.mexicanTrainItem_Click);
                computerTrainItem.Click -= new System.EventHandler(this.computerTrainItem_Click);
                myTrainItem.Click -= new System.EventHandler(this.myTrainItem_Click);

                if (mexicanTrain.IsPlayable(userHand, userDominoInPlay, out mustFlip))
                {
                    mexicanTrainItem.ForeColor = Color.Green;
                    mexicanTrainItem.Click += new System.EventHandler(this.mexicanTrainItem_Click);
                }
                else
                {
                    mexicanTrainItem.ForeColor = Color.Red;
                } 
                if (computerTrain.IsPlayable(userHand, userDominoInPlay, out mustFlip))
                {
                    computerTrainItem.ForeColor = Color.Green;
                    computerTrainItem.Click += new System.EventHandler(this.computerTrainItem_Click);
                }
                else
                {
                    computerTrainItem.ForeColor = Color.Red;
                }
                if (userTrain.IsPlayable(userHand, userDominoInPlay, out mustFlip))
                {
                    myTrainItem.ForeColor = Color.Green;
                    myTrainItem.Click += new System.EventHandler(this.myTrainItem_Click);
                }
                else
                {
                    myTrainItem.ForeColor = Color.Red;
                }
            }
        }

        // displays the context sensitve menu with the list of trains
        // sets the instance variables indexOfDominoInPlay and userDominoInPlay
        private void handPB_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox handPB = (PictureBox)sender;
            indexOfDominoInPlay = userHandPBs.IndexOf(handPB);
            if (indexOfDominoInPlay != -1)
            {
                userDominoInPlay = userHand[indexOfDominoInPlay];
                if (e.Button == MouseButtons.Right)
                {
                    whichTrainMenu.Show(handPB, 
                        handPB.Size.Width - 20, handPB.Size.Height - 20);
                }
            }
        }

        private void mexicanTrainItem_Click(object sender, EventArgs e)
        {
            // play on the mexican train
            // lets the computer take a move 
            // enables hand pbs so the user can make the next move.
            UserPlayOnTrain(userDominoInPlay, mexicanTrain, mexicanTrainPBs);
            CompleteComputerMove();
            EnableUserMove();
        }

        private void computerTrainItem_Click(object sender, EventArgs e)
        {
            // play on the computer train, lets the computer take a move and then enables
            // hand pbs so the user can make the next move.
            UserPlayOnTrain(userHand[indexOfDominoInPlay], computerTrain, computerTrainPBs);
            CompleteComputerMove();
            EnableUserMove();
        }

        private void myTrainItem_Click(object sender, EventArgs e)
        {
            // play on the user train, lets the computer take a move and then enables
            // hand pbs so the user can make the next move.
            UserPlayOnTrain(userHand[indexOfDominoInPlay], userTrain, userTrainPBs);
            CompleteComputerMove();
            EnableUserMove();
        }

        // tear down and then set up
        private void newHandButton_Click(object sender, EventArgs e)
        {
            TearDown();
            SetUp();
        }

        private void drawButton_Click(object sender, EventArgs e)
        {
            // draw a domino, add it to the hand, create a new pb and enable the new pb
            userHand.Draw(boneYard);
            userHandPBs.Add(CreateUserHandPB(nextDrawIndex));
            EnableHandPB(userHandPBs.Last());
            UpdateUI();
        }

        // open the user's train, update the ui and let the computer make a move
        // enable the hand pbs so the user can make a move
        private void passButton_Click(object sender, EventArgs e)
        {
            userTrain.Open();
            UpdateUI();
            CompleteComputerMove();
            EnableUserMove();
        }

        private void PlayMTDRightClick_Load(object sender, EventArgs e)
        {
            // register the boneyard almost empty event and it's delegate here
            SetUp();
            boneYard.AlmostEmpty += new BoneYard.EmptyHandler(RespondToEmpty);
        }

		// event handler for handling the boneyard almost empty event
        private void RespondToEmpty(BoneYard by)
        {
            MessageBox.Show("The Boneyard must be empty");
        }

    }
}
