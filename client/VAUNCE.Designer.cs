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
            this.components = new System.ComponentModel.Container();
            this.renderArea = new System.Windows.Forms.Label();
            this.timerRender = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // renderArea
            // 
            this.renderArea.BackColor = System.Drawing.Color.White;
            this.renderArea.Location = new System.Drawing.Point(279, 135);
            this.renderArea.Name = "renderArea";
            this.renderArea.Size = new System.Drawing.Size(1000, 400);
            this.renderArea.TabIndex = 3;
            // 
            // timerRender
            // 
            this.timerRender.Interval = 16;
            this.timerRender.Tick += new System.EventHandler(this.timerRender_Tick);
            // 
            // VAUNCE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1517, 748);
            this.Controls.Add(this.renderArea);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "VAUNCE";
            this.Text = "VAUNCE";
            this.Load += new System.EventHandler(this.VAUNCE_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VAUNCE_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label renderArea;
        private System.Windows.Forms.Timer timerRender;
    }
}

