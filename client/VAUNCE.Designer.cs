namespace client
{
    partial class VAUNCE
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnMusic = new System.Windows.Forms.Button();
            this.btnJump = new System.Windows.Forms.Button();
            this.btnDie = new System.Windows.Forms.Button();
            this.renderArea = new System.Windows.Forms.Label();
            this.btnDraw = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMusic
            // 
            this.btnMusic.Location = new System.Drawing.Point(1430, 623);
            this.btnMusic.Name = "btnMusic";
            this.btnMusic.Size = new System.Drawing.Size(75, 23);
            this.btnMusic.TabIndex = 0;
            this.btnMusic.Text = "music";
            this.btnMusic.UseVisualStyleBackColor = true;
            this.btnMusic.Click += new System.EventHandler(this.btnMusic_Click);
            // 
            // btnJump
            // 
            this.btnJump.Location = new System.Drawing.Point(1430, 652);
            this.btnJump.Name = "btnJump";
            this.btnJump.Size = new System.Drawing.Size(75, 23);
            this.btnJump.TabIndex = 1;
            this.btnJump.Text = "jump";
            this.btnJump.UseVisualStyleBackColor = true;
            this.btnJump.Click += new System.EventHandler(this.btnJump_Click);
            // 
            // btnDie
            // 
            this.btnDie.Location = new System.Drawing.Point(1430, 681);
            this.btnDie.Name = "btnDie";
            this.btnDie.Size = new System.Drawing.Size(75, 23);
            this.btnDie.TabIndex = 2;
            this.btnDie.Text = "die";
            this.btnDie.UseVisualStyleBackColor = true;
            this.btnDie.Click += new System.EventHandler(this.btnDie_Click);
            // 
            // renderArea
            // 
            this.renderArea.BackColor = System.Drawing.Color.White;
            this.renderArea.Location = new System.Drawing.Point(293, 105);
            this.renderArea.Name = "renderArea";
            this.renderArea.Size = new System.Drawing.Size(1024, 512);
            this.renderArea.TabIndex = 3;
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(1430, 710);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(75, 23);
            this.btnDraw.TabIndex = 4;
            this.btnDraw.Text = "draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // VAUNCE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1517, 748);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.btnDie);
            this.Controls.Add(this.btnJump);
            this.Controls.Add(this.btnMusic);
            this.Controls.Add(this.renderArea);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "VAUNCE";
            this.Text = "VAUNCE";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMusic;
        private System.Windows.Forms.Button btnJump;
        private System.Windows.Forms.Button btnDie;
        private System.Windows.Forms.Label renderArea;
        private System.Windows.Forms.Button btnDraw;
    }
}

