namespace Cloud_Vibe.Areas.Administration.ViewModels
{
    using AutoMapper;
    using Cloud_Vibe.Data.Models;
    using Cloud_Vibe.Web.Infrastructure.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class AlbumViewModel : IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int? ID { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Artist")]
        public string Artist { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }


        [Display(Name = "Shared On")]
        public DateTime SharedOn { get; set; }

        [Display(Name = "Shared By")]
        public string UserShared { get; set; }

        [Display(Name = "Video URL")]
        public string VideoLink { get; set; }

        [Display(Name = "Downloads")]
        public int Downloads { get; set; }

        [Display(Name = "Views")]
        public int Views { get; set; }

        #region IHaveCustomMappings Members

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Album, AlbumViewModel>()
                .ForMember(m => m.UserShared, opt => opt.MapFrom(u => u.UserShared.UserName));
            configuration.CreateMap<Album, AlbumViewModel>()
                .ForMember(m => m.Artist, opt => opt.MapFrom(u => u.Artist.Name));
        }

        #endregion
    }
}