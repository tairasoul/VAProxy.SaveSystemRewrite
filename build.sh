msbuild -p:Configuration=Release
mkdir JsonRewrite_spiteful
cd bin/Release/net48
declare -a copy=("JsonRewrite.dll" "Newtonsoft.Json.dll")
for i in "${copy[@]}"
do
    cp "$i" ../../../JsonRewrite_spiteful
done
cd ../../../
rm -rf "/home/eva/.local/share/Steam/steamapps/common/VA Proxy Demo/BepInEx/plugins/JsonRewrite_spiteful"
cp -r JsonRewrite_spiteful "/home/eva/.local/share/Steam/steamapps/common/VA Proxy Demo/BepInEx/plugins"
rm -rf JsonRewrite_spiteful