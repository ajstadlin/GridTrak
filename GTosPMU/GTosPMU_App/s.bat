rmdir /s /q bin\debug
rmdir /s /q obj
svn add * --force
svn status
svn commit -m %1