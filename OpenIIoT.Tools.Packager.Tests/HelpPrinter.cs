/*
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀  ▀  ▀      ▀▀
      █
      █      ▄█    █▄                                    ▄███████▄
      █     ███    ███                                  ███    ███
      █     ███    ███      ▄█████  █          █████▄   ███    ███    █████  █  ██▄▄▄▄      ██       ▄█████    █████
      █    ▄███▄▄▄▄███▄▄   ██   █  ██         ██   ██   ███    ███   ██  ██ ██  ██▀▀▀█▄ ▀███████▄   ██   █    ██  ██
      █   ▀▀███▀▀▀▀███▀   ▄██▄▄    ██         ██   ██ ▀█████████▀   ▄██▄▄█▀ ██▌ ██   ██     ██  ▀  ▄██▄▄     ▄██▄▄█▀
      █     ███    ███   ▀▀██▀▀    ██       ▀██████▀    ███        ▀███████ ██  ██   ██     ██    ▀▀██▀▀    ▀███████
      █     ███    ███     ██   █  ██▌    ▄   ██        ███          ██  ██ ██  ██   ██     ██      ██   █    ██  ██
      █     ███    █▀      ███████ ████▄▄██  ▄███▀     ▄████▀        ██  ██ █    █   █     ▄██▀     ███████   ██  ██
      █
      █       ███
      █   ▀█████████▄
      █      ▀███▀▀██    ▄█████   ▄█████     ██      ▄█████
      █       ███   ▀   ██   █    ██  ▀  ▀███████▄   ██  ▀
      █       ███      ▄██▄▄      ██         ██  ▀   ██
      █       ███     ▀▀██▀▀    ▀███████     ██    ▀███████
      █       ███       ██   █     ▄  ██     ██       ▄  ██
      █      ▄████▀     ███████  ▄████▀     ▄██▀    ▄████▀
      █
 ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄  ▄▄ ▄▄   ▄▄▄▄ ▄▄     ▄▄     ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄ ▄
 █████████████████████████████████████████████████████████████ ███████████████ ██  ██ ██   ████ ██     ██     ████████████████ █ █
      ▄
      █  Unit tests for the HelpPrinter class.
      █
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀ ▀ ▀▀▀     ▀▀               ▀
      █  The GNU Affero General Public License (GNU AGPL)
      █
      █  Copyright (C) 2016-2017 JP Dillingham (jp@dillingham.ws)
      █
      █  This program is free software: you can redistribute it and/or modify
      █  it under the terms of the GNU Affero General Public License as published by
      █  the Free Software Foundation, either version 3 of the License, or
      █  (at your option) any later version.
      █
      █  This program is distributed in the hope that it will be useful,
      █  but WITHOUT ANY WARRANTY; without even the implied warranty of
      █  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
      █  GNU Affero General Public License for more details.
      █
      █  You should have received a copy of the GNU Affero General Public License
      █  along with this program.  If not, see <http://www.gnu.org/licenses/>.
      █
      ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀  ▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀██
                                                                                                   ██
                                                                                               ▀█▄ ██ ▄█▀
                                                                                                 ▀████▀
                                                                                                   ▀▀                            */

using System;
using Xunit;

namespace OpenIIoT.Tools.Packager.Tests
{
    /// <summary>
    ///     Unit tests for the HelpPrinter class.
    /// </summary>
    public class HelpPrinter
    {
        #region Public Methods

        /// <summary>
        ///     Tests the <see cref="Packager.HelpPrinter.PrintHelp(string)"/> method.
        /// </summary>
        [Fact]
        public void PrintHelp()
        {
            Packager.HelpPrinter.PrintHelp();
            Packager.HelpPrinter.PrintHelp(string.Empty);
            Packager.HelpPrinter.PrintHelp(null);
            Packager.HelpPrinter.PrintHelp("manifest");
            Packager.HelpPrinter.PrintHelp("extract-manifest");
            Packager.HelpPrinter.PrintHelp("package");
            Packager.HelpPrinter.PrintHelp("extract-package");
            Packager.HelpPrinter.PrintHelp("verify");
            Packager.HelpPrinter.PrintHelp("trust");
            Packager.HelpPrinter.PrintHelp(Guid.NewGuid().ToString());
        }

        #endregion Public Methods
    }
}