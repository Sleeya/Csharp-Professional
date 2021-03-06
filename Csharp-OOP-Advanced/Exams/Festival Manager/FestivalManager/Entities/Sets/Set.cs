﻿namespace FestivalManager.Entities.Sets
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using Contracts;

	public abstract class Set:ISet
	{
		private readonly List<IPerformer> performers;
		private readonly List<ISong> songs;

        protected Set(string name, TimeSpan maxDuration)
		{
			this.Name = name;
			this.MaxDuration = maxDuration;

			this.performers = new List<IPerformer>();
			this.songs = new List<ISong>();
		}

		public string Name { get; }

		public TimeSpan MaxDuration { get; }

		public TimeSpan ActualDuration => new TimeSpan(this.Songs.Sum(s => s.Duration.Ticks));

		public IReadOnlyCollection<IPerformer> Performers
		{
			get { return this.performers; }
		}

		public IReadOnlyCollection<ISong> Songs
		{
			get { return songs; }
		}

        public void AddPerformer(IPerformer performer)
        {
            this.performers.Add(performer);
        }

		public void AddSong(ISong song)
		{
			if (song.Duration + this.ActualDuration > this.MaxDuration)
			{
				throw new InvalidOperationException("Song is over the set limit!");
			}

			this.songs.Add(song);
		}

		public bool CanPerform()
		{
            bool hasPerformers = this.Performers.Any();
            bool hasSongs = this.Songs.Any();
            bool performersHaveAtleastOneNotBrokenInstrument = this.Performers.All(p => p.Instruments.Any(i => i.IsBroken == false));

            return hasPerformers && hasSongs && performersHaveAtleastOneNotBrokenInstrument;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();

			sb.AppendLine(string.Join(", ", this.Performers));

			foreach (var song in this.Songs)
			{
				sb.AppendLine($"-- {song}");
			}

			var result = sb.ToString();
			return result;
		}
	}
}
