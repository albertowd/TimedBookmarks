using System;
using System.Collections.Generic;
using System.Linq;

namespace TimedBookmarks
{
    class Bookmarks
    {
        /// <summary>
        /// Start time for keep track of the TimeSpans.
        /// </summary>
        private DateTime startTime;

        /// <summary>
        /// List of bookmarks saved through time.
        /// </summary>
        private List<TimeSpan> bookmarks;

        /// <summary>
        /// Default constructor, just reset the startTime.
        /// </summary>
        public Bookmarks()
        {
            bookmarks = new List<TimeSpan>();
            this.Start();
        }

        /// <summary>
        /// Verifies if the bookmark list is empty.
        /// </summary>
        /// <returns>True if its empty.</returns>
        public bool IsEmpty()
        {
            return this.bookmarks.Count == 0;
        }

        /// <summary>
        /// Returns the StartTime string.
        /// </summary>
        /// <returns>StartTime string.</returns>
        public string GetStartTime()
        {
            return this.startTime.ToString("HH:mm:ss");
        }

        /// <summary>
        /// Returns the serialized content of the bookmarks.
        /// </summary>
        /// <returns>Text showing the startTime and the bookmarks timespan from startTime.</returns>
        public string Serialize()
        {
            string content = "Start Time: " + this.startTime.ToString() + "\n";
            if (this.bookmarks.Count > 0)
                for (int index = 1; index < this.bookmarks.Count; ++index)
                    content += this.bookmarks[index].ToString() + "\n";
            else
                content += "No bookmarks recorded.\n";
            return content;
        }

        /// <summary>
        /// Save a bookmark timespan.
        /// </summary>
        /// <returns>The current bookmark.</returns>
        public TimeSpan Bookmark()
        {
            TimeSpan ts = DateTime.Now.Subtract(this.startTime);
            this.bookmarks.Add(ts);
            return ts;
        }

        /// <summary>
        /// Get a saved Bookmark.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The index Bookmark or an empty timespan.</returns>
        public TimeSpan GetBookmark(int index)
        {
            if (index < this.bookmarks.Count)
                return this.bookmarks[index];
            else return new TimeSpan(0);
        }

        /// <summary>
        /// Clean saved bookmarks and reset startTime.
        /// </summary>
        /// <param name="startOver">True to update the startTime.</param>
        public void Reset(bool startOver)
        {
            this.bookmarks.Clear();
            if (startOver)
                this.startTime = DateTime.Now;
        }

        /// <summary>
        /// Update the startTime of the bookmarks.
        /// </summary>
        public void Start()
        {
            this.startTime = DateTime.Now;
        }
    }
}
