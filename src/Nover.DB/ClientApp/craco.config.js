/* craco.config.js */
// const apiMocker = require('mocker-api')// 可以使用mocker-api库
const path = require('path')
const pathResolve = pathUrl => path.join(__dirname, pathUrl)
const name = 'React Element Admin'

module.exports = {
  publicPath: './',
  devServer: {
    before: require('./mock/mock-server'),
    proxy: {
      '/api': {
        target: 'http://127.0.0.1:8081',
        changeOrigin: true,
        pathRewrite: {
          '^/api': ''
        }
      }
    }

  },
  webpack: {
    alias: {
      '@': pathResolve('src')
    },
    configure: (webpackConfig) => {
      webpackConfig.plugins[0].options.title = name
      const oneOfRule = webpackConfig.module.rules.find((rule) => rule.oneOf)
      if (oneOfRule) {
        oneOfRule.oneOf.splice(0, 0, {
          test: /\.svg$/,
          include: path.resolve(__dirname, 'src/icons'),
          use: [{
            loader: 'svg-sprite-loader',
            options: {
              extract: false,
              symbolId: 'icon-[name]'
            }
          }]
        })
      }
      return webpackConfig
    }
  }
}
