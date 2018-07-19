using System;
using System.Collections.Generic;

namespace Main
{
	public class TaskModel
	{
		public string Description { get; set; }
		public DateTime Start { get; set; }
		public DateTime? End { get; set; }
		public bool? AlldayTask { get; set; }
		public bool? ImportantTask { get; set; }

		public TaskModel(string description, DateTime start, DateTime? end, bool? allDayTask, bool? importantTask)
		{
			Description = description;
			Start = start;
			End = end;
			AlldayTask = allDayTask;
			ImportantTask = importantTask;
		}
	}
}
