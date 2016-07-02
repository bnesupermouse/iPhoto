var gulp = require('gulp');
var ts = require('gulp-typescript');

gulp.task('tsc', function () {
    gulp.src('WebSrc/app/**/*.ts')
        .pipe(ts())
        .pipe(gulp.dest('WebSrc/javascripts'));
});