/**
 * Welcome to your Workbox-powered service worker!
 *
 * You'll need to register this file in your web app and you should
 * disable HTTP caching for this file too.
 * See https://goo.gl/nhQhGp
 *
 * The rest of the code is auto-generated. Please don't update this file
 * directly; instead, make changes to your Workbox build configuration
 * and re-run your build process.
 * See https://goo.gl/2aRDsh
 */

importScripts("https://storage.googleapis.com/workbox-cdn/releases/4.3.1/workbox-sw.js");

self.addEventListener('message', (event) => {
  if (event.data && event.data.type === 'SKIP_WAITING') {
    self.skipWaiting();
  }
});

/**
 * The workboxSW.precacheAndRoute() method efficiently caches and responds to
 * requests for URLs in the manifest.
 * See https://goo.gl/S9QRab
 */
self.__precacheManifest = [
  {
    "url": "404.html",
    "revision": "3235b84cea6aaa1e25e75cc2f1744304"
  },
  {
    "url": "assets/css/0.styles.f730c823.css",
    "revision": "e1fe8e49396e157106a91e2e6551fd00"
  },
  {
    "url": "assets/img/ConnectUser.90bd363d.png",
    "revision": "90bd363da945943c39caf87962c3440c"
  },
  {
    "url": "assets/img/DeleteProject.8b22e81c.png",
    "revision": "8b22e81c3cd0583ade0a7b8e661a4f95"
  },
  {
    "url": "assets/img/DeleteTask.9e7ae713.png",
    "revision": "9e7ae713e07b1d77b443e77be4f35c82"
  },
  {
    "url": "assets/img/DeleteUser.432065d9.png",
    "revision": "432065d9d027e852af02d1e8ee844068"
  },
  {
    "url": "assets/img/GetAllProjects.843c3c9a.png",
    "revision": "843c3c9ae177d69643363723e8155a51"
  },
  {
    "url": "assets/img/GetProject.f101dbba.png",
    "revision": "f101dbba89cc49cf37b7e7c53de81e05"
  },
  {
    "url": "assets/img/GetReview.ed03d624.png",
    "revision": "ed03d6247f666ec10d52c536e94a62d2"
  },
  {
    "url": "assets/img/GetTask.0808a4c8.png",
    "revision": "0808a4c88ba1e61a06725d756248a100"
  },
  {
    "url": "assets/img/GetTasks.a68d300f.png",
    "revision": "a68d300f973c65826e7c6ebe8ec9882a"
  },
  {
    "url": "assets/img/GetUser.30b296ba.png",
    "revision": "30b296ba37398d4e674732080139eca1"
  },
  {
    "url": "assets/img/GetUsers.8ebd0ac2.png",
    "revision": "8ebd0ac28656ca5808833b4b20a8db30"
  },
  {
    "url": "assets/img/PatchProject.1c782d5f.png",
    "revision": "1c782d5fd99ec66cc4041ece6a7faf0e"
  },
  {
    "url": "assets/img/PatchTask.1c82ab74.png",
    "revision": "1c82ab74a010d72c655fac603a13f3f4"
  },
  {
    "url": "assets/img/PatchUser.3ec72559.png",
    "revision": "3ec725599008dcd15acc4bfa14358fa3"
  },
  {
    "url": "assets/img/PostProject.d43572d5.png",
    "revision": "d43572d5fe0918f1526ba807de67c7e2"
  },
  {
    "url": "assets/img/PostReview.17deaaa2.png",
    "revision": "17deaaa27aa16bba198e3001d9999c3f"
  },
  {
    "url": "assets/img/PostTask.eca018df.png",
    "revision": "eca018dff3c196985141fbbc95cef993"
  },
  {
    "url": "assets/img/search.83621669.svg",
    "revision": "83621669651b9a3d4bf64d1a670ad856"
  },
  {
    "url": "assets/js/10.6e2d7b26.js",
    "revision": "04a2d21322f5f843593d6d397b254183"
  },
  {
    "url": "assets/js/11.5fc153fd.js",
    "revision": "071c38fff89229e41e433fd3b4602a35"
  },
  {
    "url": "assets/js/12.a3171245.js",
    "revision": "cc6d83db0072a03f92c1b37982a65479"
  },
  {
    "url": "assets/js/13.28e62a03.js",
    "revision": "de4d314e4761ea1422c3a7bd2f124454"
  },
  {
    "url": "assets/js/14.8ab404a4.js",
    "revision": "ee3bb247c453bacb6711510d6405bf5f"
  },
  {
    "url": "assets/js/15.29024b37.js",
    "revision": "61f6cb77166a8558fab96173a22fecfb"
  },
  {
    "url": "assets/js/16.a7ae4383.js",
    "revision": "f22a4f3715eaf81fc826a8146a43f56b"
  },
  {
    "url": "assets/js/17.9e27a14f.js",
    "revision": "0a225d08c64ac6dc0fde141e1462fe79"
  },
  {
    "url": "assets/js/18.08ed11b6.js",
    "revision": "fda6834adcad29b2515b49a22b4098e0"
  },
  {
    "url": "assets/js/19.a9f2d87d.js",
    "revision": "a067e26049c6c5494cb826e2f4203ac8"
  },
  {
    "url": "assets/js/2.a2292618.js",
    "revision": "7cc14d779a8fd040d0a9db1bd024de1b"
  },
  {
    "url": "assets/js/20.25929138.js",
    "revision": "0098fb4b4a74473afac06e452ffb675e"
  },
  {
    "url": "assets/js/21.3fd60d9a.js",
    "revision": "5c448b91f1a132bea53c253d2fb0ddac"
  },
  {
    "url": "assets/js/22.4d4e9acd.js",
    "revision": "2903a83ae666dfda08aa839db34bad8d"
  },
  {
    "url": "assets/js/23.9844066a.js",
    "revision": "310d22c85d600e148bd037635e74933a"
  },
  {
    "url": "assets/js/24.45de6992.js",
    "revision": "dae3c229aafaf4484472f323415c3e2a"
  },
  {
    "url": "assets/js/26.d4420a7c.js",
    "revision": "1e186a7439ba927e6e7f5b83eacb8afe"
  },
  {
    "url": "assets/js/3.2488a23b.js",
    "revision": "9c7f03d851293deb49753b5e4dedb461"
  },
  {
    "url": "assets/js/4.f4e3418f.js",
    "revision": "552a2580fd25b4140ef34bd0b282972c"
  },
  {
    "url": "assets/js/5.009de7c3.js",
    "revision": "dc584098e4f29321b75c07a9133a1bde"
  },
  {
    "url": "assets/js/6.b8ae5079.js",
    "revision": "d82da7838d9726e9a5cb79129c8a685b"
  },
  {
    "url": "assets/js/7.bc58ede0.js",
    "revision": "d52066b3266975461561da369e2d077a"
  },
  {
    "url": "assets/js/8.7fa37b39.js",
    "revision": "116b0fa2a0e7b71e38b9cb4fe90d629c"
  },
  {
    "url": "assets/js/9.d2fe2f72.js",
    "revision": "6e7aa7e37522b3a6cef8ee65f1360774"
  },
  {
    "url": "assets/js/app.3c35f41a.js",
    "revision": "04157f299fc8f1ec06259ad2a946f3e3"
  },
  {
    "url": "conclusion/index.html",
    "revision": "ad103ed0da2c5433852ad5b44befe286"
  },
  {
    "url": "design/index.html",
    "revision": "1cc346817a2fb556b4a5e23bcef8e652"
  },
  {
    "url": "index.html",
    "revision": "3fd445ccc2846584ef58ee59792ac6d2"
  },
  {
    "url": "intro/index.html",
    "revision": "2ed9a654a04a9862b2651a393062bf8c"
  },
  {
    "url": "license.html",
    "revision": "70a2edbdcd1a16b29dc6ec11e14ab937"
  },
  {
    "url": "myAvatar.png",
    "revision": "b76db1e62eb8e7fca02a487eb3eac10c"
  },
  {
    "url": "requirements/index.html",
    "revision": "76688dee3e74d14c6b08aaa0ab08e4a6"
  },
  {
    "url": "requirements/stakeholders-needs.html",
    "revision": "810bc4f54d78824983f0d8c3ec6d9795"
  },
  {
    "url": "requirements/state-of-the-art.html",
    "revision": "6f135f111378b0f661aa83c185cd51d7"
  },
  {
    "url": "software/index.html",
    "revision": "a4576e19f137c518d7618d23edfae50f"
  },
  {
    "url": "test/index.html",
    "revision": "b71a8127cdf8f8d87a03988e45132b99"
  },
  {
    "url": "use cases/index.html",
    "revision": "a6e277ab99c38a6f9cd73288fc2756e9"
  }
].concat(self.__precacheManifest || []);
workbox.precaching.precacheAndRoute(self.__precacheManifest, {});
addEventListener('message', event => {
  const replyPort = event.ports[0]
  const message = event.data
  if (replyPort && message && message.type === 'skip-waiting') {
    event.waitUntil(
      self.skipWaiting().then(
        () => replyPort.postMessage({ error: null }),
        error => replyPort.postMessage({ error })
      )
    )
  }
})
