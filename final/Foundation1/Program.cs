using System;
using System.Collections.Generic;

class Comment
{
    public string CommenterName { get; set; }
    public string CommentText { get; set; }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    public List<Comment> Comments { get; }

    public Video()
    {
        Comments = new List<Comment>();
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }
}

class VideoManager
{
    private List<Video> videos;

    public VideoManager()
    {
        videos = new List<Video>();
    }

    public Video CreateVideo(string title, string author, int length)
    {
        Video video = new Video
        {
            Title = title,
            Author = author,
            Length = length
        };
        videos.Add(video);
        return video;
    }

    public void AddCommentToVideo(Video video, string commenterName, string commentText)
    {
        Comment comment = new Comment
        {
            CommenterName = commenterName,
            CommentText = commentText
        };
        video.Comments.Add(comment);
    }

    public int GetVideoCommentCount(Video video)
    {
        return video.GetCommentCount();
    }

    public string GetVideoInformation(Video video)
    {
        string videoInformation = $"Title: {video.Title}\nAuthor: {video.Author}\nLength (seconds): {video.Length}\n";
        videoInformation += $"Number of Comments: {GetVideoCommentCount(video)}\n";
        videoInformation += "Comments:\n";
        foreach (Comment comment in video.Comments)
        {
            videoInformation += $"  Commenter: {comment.CommenterName}\n  Comment Text: {comment.CommentText}\n";
        }
        return videoInformation;
    }
}

class CommentManager
{
    public Comment CreateComment(string commenterName, string commentText)
    {
        return new Comment
        {
            CommenterName = commenterName,
            CommentText = commentText
        };
    }

    public string GetCommentInformation(Comment comment)
    {
        return $"Commenter: {comment.CommenterName}\nComment Text: {comment.CommentText}";
    }
}

class AbstractionwithYouTubeVideos
{
    static void Main(string[] args)
    {
        VideoManager videoManager = new VideoManager();
        CommentManager commentManager = new CommentManager();

        // Create videos
        Console.WriteLine();
        Video video1 = videoManager.CreateVideo("Video 1", "Author 1", 120);
        Video video2 = videoManager.CreateVideo("Video 2", "Author 2", 180);
        Video video3 = videoManager.CreateVideo("Video 3", "Author 3", 90);

        // Add comments to videos
        videoManager.AddCommentToVideo(video1, "User A", "Great video!");
        videoManager.AddCommentToVideo(video1, "User B", "I learned a lot.");
        videoManager.AddCommentToVideo(video1, "User C", "Could have been better.");

        videoManager.AddCommentToVideo(video2, "User D", "Very informative!");
        videoManager.AddCommentToVideo(video2, "User E", "Liked the examples.");
        videoManager.AddCommentToVideo(video2, "User F", "Loved the examples.");

        videoManager.AddCommentToVideo(video3, "User G", "Short and sweet.");
        videoManager.AddCommentToVideo(video3, "User I", "Concise and relevant.");
        videoManager.AddCommentToVideo(video3, "User J", "On-point and impactful.");

        // Display video information and comments
        List<Video> videos = new List<Video> { video1, video2, video3 };
        foreach (Video video in videos)
        {
            Console.WriteLine(videoManager.GetVideoInformation(video));
            Console.WriteLine();
        }
    }
}

// This program is a YouTube video management system that allows users to create videos,  
// add comments to videos, and retrieve information about the videos and comments. 