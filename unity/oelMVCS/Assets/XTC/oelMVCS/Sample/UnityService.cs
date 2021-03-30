using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace XTC.oelMVCS
{

    public class UnityService : Service
    {
        public MonoBehaviour mono;

        protected override void asyncRequest(string _url, string _method, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
        {
            mono.StartCoroutine(request(_url, _method, _params, _onReply, _onError, _options));
        }

        protected virtual IEnumerator request(string _url, string _method, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
        {
            string url = _url;
            getLogger().Debug("request: {0}", url);
            var request = new UnityWebRequest(url, _method);

            if (null == jsonPack)
                throw new System.NotImplementedException("jsonPack is null");

            string json = jsonPack(_params);

            byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(json);

            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(postBytes);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");
            if (null != _options)
            {
                foreach (KeyValuePair<string, string> pair in _options.header)
                {
                    request.SetRequestHeader(pair.Key, pair.Value);
                }
            }

            if (PlayerPrefs.HasKey("jwt-token"))
            {
                request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("jwt-token"));
            }
            yield return request.SendWebRequest();

            if (request.responseCode != 200)
            {
                if (null != _onError)
                    _onError(Error.NewAccessErr(string.Format("status code: {0}", request.responseCode)));
                yield break;
            }

            string text = request.downloadHandler.text;
            if (null != _onReply)
                _onReply(text);
        }
    }
}//namespace
