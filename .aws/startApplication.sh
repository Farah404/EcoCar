#!/bin/bash
# to remove once you set bash_profile correctly
export DOTNET_ROOT=/tmp/dotnet
export PATH=$PATH:$DOTNET_ROOT
# end to remove
Env=$(cat /tmp/env.txt)
ApplicationName=$(cat /tmp/application_name.txt)
DllToStart=$(cat /tmp/dll_to_start.txt)
echo ${Env}
echo ${ApplicationName}
echo ${DllToStart} 
# we remove first and last quote get with ssm
cd /var/www/
mkdir ${ApplicationName}
sudo cp -R /var/www/my-temp-dir/* /var/www/${ApplicationName}/
echo "start application"
cd /var/www/${ApplicationName}/
echo "dotnet ${DllToStart} > /dev/null 2>&1 &"
dotnet ${DllToStart} > /dev/null 2>&1 &
cd  /var/www/my-temp-dir/
rm -rf *