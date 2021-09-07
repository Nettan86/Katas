using System;
using System.Collections.Generic;
using Scheduler.Exceptions;

namespace Scheduler.Models
{
    public class CaseWorker
    {
        public string Name;
        public List<Meeting> Meetings;

        public CaseWorker()
        {
            Meetings = new List<Meeting>();

            DateTime startOfWork = DateTime.Today.AddHours(8);
            for (int i = 0; i < 6; i++)
            {
                DateTime startOfMeeting = startOfWork.AddHours(i);
                Meeting meeting = new Meeting(startOfMeeting);

                Meetings.Add(meeting);
            }
        }

        public void NewDateAdded(DateTime start)
        {
            Meeting newMeeting = new Meeting(start);

            foreach (Meeting meeting in Meetings)
            {
                if (meeting.Overlap(newMeeting))
                {
                    throw new MeetingOverlapException(meeting);
                }
            }

            Meetings.Add(newMeeting);
        }

        public void ChangeMeeting(int index, DateTime newStart)
        {
            Meeting meetingToChange = Meetings[index];
            Meeting attemptMeeting = new Meeting(newStart, meetingToChange.Duration);

            foreach (Meeting meeting in Meetings)
            {
                if (meeting == meetingToChange)
                    continue;

                if (meeting.Overlap(attemptMeeting))
                {
                    throw new MeetingOverlapException(meeting);
                }
                
            }

            meetingToChange.Start = newStart;
        }
    }
}
