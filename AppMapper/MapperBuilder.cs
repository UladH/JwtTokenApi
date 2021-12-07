using AppMapper.Profiles;
using AutoMapper;

namespace AppMapper
{
    public static class MapperBuilder
    {
        #region public

        public static IMapper Create()
        {
            var config = new MapperConfiguration(mc => {
                mc.AddProfile(new UserProfile());
            });

            return config.CreateMapper();
        }

        #endregion
    }
}
