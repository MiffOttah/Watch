if (args.Length != 1)
{
    Console.Error.WriteLine("watch FILENAME");
    Environment.ExitCode = 3;
}
else if (args[0] == "--help")
{
    Console.Out.WriteLine(@"watch FILENAME
Will run until the file specified is modified.


Copyright (C) 2023 Miff <miffthefox.info>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.");
}
else if (!File.Exists(args[0]))
{
    Console.Error.WriteLine("File not found: " + args[0]);
    Environment.ExitCode = 2;
}
else
{
    try
    {
        string fullPath = Path.GetFullPath(args[0]);
        var watcher = new FileSystemWatcher(Path.GetDirectoryName(fullPath) ?? throw new Exception("Not a valid path."), Path.GetFileName(fullPath));
        watcher.WaitForChanged(WatcherChangeTypes.All);
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine("watch: " + ex.Message);
        Environment.ExitCode = 1;
    }
}