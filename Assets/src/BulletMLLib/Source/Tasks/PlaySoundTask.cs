using System.Diagnostics;

namespace BulletMLLib
{
	public class PlaySoundTask : BulletMLTask
	{
		#region Methods

        private string _soundName;

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletMLLib.BulletMLTask"/> class.
		/// </summary>
		/// <param name="node">Node.</param>
		/// <param name="owner">Owner.</param>
        public PlaySoundTask(PlaySoundNode node, BulletMLTask owner)
            : base(node, owner)
		{
			System.Diagnostics.Debug.Assert(null != Node);
			System.Diagnostics.Debug.Assert(null != Owner);
		}

        protected override void SetupTask(Bullet bullet)
        {
            _soundName = Node.Text;
        }

		/// <summary>
		/// Run this task and all subtasks against a bullet
		/// This is called once a frame during runtime.
		/// </summary>
		/// <returns>ERunStatus: whether this task is done, paused, or still running</returns>
		/// <param name="bullet">The bullet to update this task against.</param>
		public override ERunStatus Run(Bullet bullet)
		{
            if (_soundName.Contains("+"))
            {
                SoundEngine.PlayRandomClip(_soundName.Split('+'));
            }
            else
            {
                SoundEngine.PlayClip(_soundName);
            }
			return ERunStatus.End;
		}

		#endregion //Methods
	}
}