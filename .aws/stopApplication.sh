#!/bin/bash
Env=$(cat /tmp/env.txt)
ApplicationName=$(cat /tmp/application_name.txt)
DllToStart=$(cat /tmp/dll_to_start.txt)
echo "stop application"
cd  /var/www/${ApplicationName}/
echo "kill $(ps aux | grep $DllToStart | awk '{print $2}')"
kill $(ps aux | grep $DllToStart | awk '{print $2}') || echo "Process $DllToStart was not running."
rm -rf /var/www/${ApplicationName}/
