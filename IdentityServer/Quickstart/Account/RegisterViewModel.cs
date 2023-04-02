﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace IdentityServerHost.Quickstart.UI
{
    public class RegisterViewModel : LoginInputModel
	{
		public string Email { get; set; } = "";
		public string RepeatPassword { get; set; } = "";
	}
}