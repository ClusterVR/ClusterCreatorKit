using System.Threading.Tasks;

namespace ClusterVR.CreatorKit.Editor.Preview.World
{
    public class AvatarRespawner
    {
        readonly float despawnHeight;
        readonly PlayerPresenter playerPresenter;

        public AvatarRespawner(float despawnHeight, PlayerPresenter playerPresenter)
        {
            this.despawnHeight = despawnHeight;
            this.playerPresenter = playerPresenter;
            CheckHeight();
        }

        async void CheckHeight()
        {
            while (playerPresenter.PlayerTransform != null)
            {
                if (playerPresenter.PlayerTransform.position.y < despawnHeight)
                {
                    playerPresenter.Respawn();
                }
                await Task.Delay(300);
            }
        }
    }

}
