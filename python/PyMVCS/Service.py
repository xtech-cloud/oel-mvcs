import urllib.request
import http.client
import json

class Service:
    def __init__(self):
        self.useMock = False
        self.mockProcessor = None

    def Setup(self, _framework):
        self.logger = _framework.logger
        self.config = _framework.config
        self.modelCenter = _framework.modelCenter
        self.serviceCenter = _framework.serviceCenter
        self._setup()

    def Dismantle(self):
        self._dismantle()

    def _post(self, _url, _params, _onReply, _onError, _options):
        self.logger.Debug('post -> %s' % (_url))
        self.logger.Debug('params is %s' % (_params))
        if self.useMock:
            self.logger.Debug('use mock ... ')
            if self.mockProcessor:
                self.mockProcessor(_url, 'POST', _params, _onReply, _onError, _options)
        else:
            try:
                data = json.dumps(_params)
                data = bytes(data, 'utf8')
                request = urllib.request.Request(_url, data, _options)
                reply = urllib.request.urlopen(request).read().decode('utf-8')
            except urllib.error.URLError as e:
                _onError(str(e))
                return
            self.logger.Debug('reply -> %s' % (reply))
            _onReply(reply)

    def _get(self, _url, _params, _onReply, _onError, _options):
        self.logger.Debug('get %s' % (_url))
        self.logger.Debug('params is %s' % (_params))
        if self.useMock:
            if self.mockProcessor:
                self.mockProcessor(_url, 'GET', _params, _onReply, _onError, _options)
        else:
            try:
                data = json.dumps(_params)
                data = bytes(data, 'utf8')
                request = urllib.request.Request(_url, _options)
                reply = urllib.request.urlopen(request).read().decode('utf-8')
            except urllib.error.URLError as e:
                _onError(str(e))
                return
            self.logger.Debug('reply -> %s' % (reply))
            _onReply(reply)

    def _setup(self):
        pass

    def _dismantle(self):
        pass
