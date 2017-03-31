# Repository Utils
Generate entity classes from exsited database, and support following databases: MsSQL, MySQL.
## Usages
``` bash
CsharpGenerator g [options]
```
options:
``` bash
  -t, --dbtype              Required. 指定数据库类型，如：mssql、mysql、oracle

  -c, --connectionstring    Required. 设置连接字符串

  -o, --out                 Required. 设置输入目录

  -n, --namespace           (Default: namespace_xxx) 设置命名空间

  -d, --dbname              Required. 指定数据库名称

  -p, --parent              指定父类

  -l, --loggerlevel         (Default: 2) 设置日志输出级别, 0-6

  --help                    Display this help screen.

  --version                 Display version information.
```
e.g.
``` bash
CsharpGenerator g -t mysql -o out -n test_namespace -d test -l 1 -c "server=172.16.20.xx;database=test;uid=root;pwd=1234;charset='utf8'"
```