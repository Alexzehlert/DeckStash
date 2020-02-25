
const webpack = require('webpack');
// Enforce same case as file system
var CaseSensitivePathsPlugin = require('case-sensitive-paths-webpack-plugin');
// Removes previous files in build folder
const CleanWebpackPlugin = require('clean-webpack-plugin');
// Inject js and css into html file
const HtmlWebpackPlugin = require('html-webpack-plugin');
// Copy the cshtml file into build folder
const WriteFilePlugin = require('write-file-webpack-plugin');


module.exports = evn => {
    const { argv } = require('yargs');
    // --p or --optimize-minimize
    const IS_PRODUCTION = argv.p || argv['optimize-minimize'];
    // --logging
    const IS_LOGGING = argv.logging;
    const HASH = IS_PRODUCTION ? '.[hash]' : '';
    const BUILD_FOLDER = `${__dirname}/../SearchMTG.web/build`;
    const HTML_FILE_NAME = '_Layout.cshtml';

    const config = {
        mode: IS_PRODUCTION ? 'production' : 'development',
        context: `${__dirname}/src`,
        entry: [ 'babel-polyfill', './app.js' ],
        output: {
            publicPath: '/build/',
            path: BUILD_FOLDER,
            filename: `bundle${HASH}.js`,
        },
        watch: true,
        module: {
            // configuration regarding modules
            rules: [
                // rules for modules (configure loaders, parser options, etc.)
                {
                    test: /\.js$/,
                    exclude: /(node_modules)/,
                    use: {
                        loader: 'babel-loader',
                        options: {
                            presets: [
                                '@babel/preset-env',
                                '@babel/preset-react'
                            ],
                            plugins: [
                                "@babel/plugin-proposal-class-properties"
                            ]
                        },
                    },
                },
                {
                    test: /\.css$/,
                    use: ["style-loader", "css-loader"]
                },
                {
                    test: /\.scss$/,
                    use: ["style-loader", "css-loader", "sass-loader"]
                },
                {
                    test: /\.(png|jpg|gif|ico)$/,
                    exclude: /node_modules/,
                    use: [ { loader: 'url-loader' } ]
                }
            ],
        },
        plugins: [
            // Enforce same case as file system
            new CaseSensitivePathsPlugin(),
            // Removes previous files in build folder
            new CleanWebpackPlugin(),
            // Inject js and css into html file
            new HtmlWebpackPlugin({
              filename: HTML_FILE_NAME,
              template: HTML_FILE_NAME,
              favicon: './../img/favicon.ico'
            }),
            new webpack.HotModuleReplacementPlugin(),
            // Copy the cshtml file into build folder
            new WriteFilePlugin({
                test: /\.cshtml$/
            })
        ],
        resolve: {
            extensions: [ '.js', '.jsx', '.scss', '.css' ],
            //Allow locating dependancies relative to the src directory and then the js directory
            modules: [
                `${__dirname}/src`,
                'node_modules'
            ]
        },
        devServer: {
            filename: 'bundle.js',
            proxy: {
                '*': {
                    target: 'https://localhost:44312/',
                    secure: false
                }
            },
            historyApiFallback: true,
            port: 8080,
            https: true
        }
    };

    if (IS_PRODUCTION)
        config.plugins.push(new webpack.DefinePlugin({ 'process.env': { NODE_ENV: '"production"' } }));
    else if (IS_LOGGING)
        config.plugins.push(new webpack.DefinePlugin({ 'process.env': { NODE_ENV: '"logging"' } }));

    return config;
};