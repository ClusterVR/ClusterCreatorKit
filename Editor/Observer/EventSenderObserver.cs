using System;
using ClusterVR.CreatorKit.Editor.Repository;
using ClusterVR.CreatorKit.Editor.Utils;
using UnityEditor;
using UnityEngine;
using PackageInfo = ClusterVR.CreatorKit.Editor.Infrastructure.PackageInfo;

namespace ClusterVR.CreatorKit.Editor.Analytics
{
    [InitializeOnLoad]
    public static class EventSenderObserver
    {
        const int IntervalSec = 60 * 5;

        static TokenAuthRepository TokenAuthRepository => TokenAuthRepository.Instance;
        static EditorPrefsRepository EditorPrefsRepository => EditorPrefsRepository.Instance;

        sealed class SessionInfo : ScriptableSingleton<SessionInfo>
        {
            [SerializeField] string sessionId;
            [SerializeField] double lastSentAt;

            public string SessionId
            {
                get
                {
                    if (string.IsNullOrEmpty(sessionId))
                    {
                        sessionId = Guid.NewGuid().ToString();
                    }

                    return sessionId;
                }
            }

            public double LastSentAt
            {
                get => lastSentAt;
                set => lastSentAt = value;
            }
        }

        static EventSenderObserver()
        {
            ReactiveBinder.Bind(TokenAuthRepository.SavedUserId, PanamaLogger.SetUserId);
            ReactiveBinder.Bind(EditorPrefsRepository.TmpUserId, PanamaLogger.SetTmpUserId);
            PanamaLogger.SetCreatorKitVersion(PackageInfo.GetCreatorKitVersion());
            EditorApplication.update += Update;
        }

        static void Update()
        {
            PanamaLogger.SendEvents();
            var now = EditorApplication.timeSinceStartup;
            if (now - SessionInfo.instance.LastSentAt < IntervalSec)
            {
                return;
            }

            EventSender.Ping(
                accessToken: TokenAuthRepository.SavedAccessToken.Val.Token,
                sessionId: SessionInfo.instance.SessionId,
                tmpUserId: GetOrCreateTmpUserId());

            SessionInfo.instance.LastSentAt = now;
        }

        static string GetOrCreateTmpUserId()
        {
            var tmpUserId = EditorPrefsRepository.TmpUserId.Val;
            if (!string.IsNullOrEmpty(tmpUserId))
            {
                return tmpUserId;
            }
            tmpUserId = Guid.NewGuid().ToString();
            EditorPrefsRepository.SetTmpUserId(tmpUserId);
            return tmpUserId;
        }
    }
}
