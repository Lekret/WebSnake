using ME.ECS;
using WebSnake.Features.Gameplay;

namespace WebSnake.Extensions
{
    public static class ViewUtils
    {
        public static IMonoViewsRepo GetViewsRepo(this World world)
        {
            return world.GetFeature<GameplayFeature>().ViewsRepo;
        }
    }
}