﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECom.Messages;
using ECom.Utility;

namespace ECom.ReadModel.Views
{
	[Serializable]
	public class UserDetails : Dto
	{
		public string ID { get; set; }
		public string Name { get; set; }
		public string PhotoUrl { get; set; }
		public string Email { get; set; }

		public UserDetails()
		{
		}

		public UserDetails(string email, string name, string photoUrl)
		{
			ID = email;
			Name = name;
			PhotoUrl = photoUrl ?? String.Empty;
			Email = String.Empty;
		}
	}

	public class UserDetailsView : ReadModelView,
		IHandle<UserLoggedInReported>,
		IHandle<UserEmailSet>
	{
		public UserDetailsView(IDtoManager manager, IReadModelFacade readModel)
			: base(manager, readModel)
		{
		}

		public void Handle(UserLoggedInReported e)
		{
			_manager.Delete<UserDetails>(e.Id);
			_manager.Add<UserDetails>(new UserDetails(e.Id.Id, e.UserName, e.PhotoUrl));
		}

		public void Handle(UserEmailSet e)
		{
			_manager.Update<UserDetails>(e.Id, ud => ud.Email = e.Email.RawAddress);
		}
	}
}
