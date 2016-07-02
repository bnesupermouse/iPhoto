var gulp = require('gulp');
var concat = require('gulp-concat');
var ts = require('gulp-typescript');

gulp.task('tsc', function () {
    var tsResult =
    gulp.src('WebSrc/app/**/**/*.ts')
        .pipe(ts());
        
    return tsResult
         .pipe(concat('js-library.js')) // You can use other plugins that also support gulp-sourcemaps
         .pipe(gulp.dest('WebSrc/app/javascripts'));
});

gulp.task('tsc:w', ['tsc'], function () {
    gulp.watch('WebSrc/app/**/**/*.ts', ['tsc']);
});