# PinkCandyReturns  
*A plugin for SCP: Secret Laboratory using EXILED*  

## 📌 Description  
PinkCandyReturns adds a **chance-based Pink Candy** to SCP-330, the candy bowl. Players have a configurable chance (default: **10%**) to receive **Pink Candy** when picking up SCP-330. Additionally, server admins can **give Pink Candy manually** using commands.  

## ⚙️ Features  
✅ Configurable chance for Pink Candy when picking up SCP-330  
✅ Admin command to manually give Pink Candy to players 
"addcandy username pink"
✅ Simple configuration in `config.yml`  
✅ Works with the EXILED plugin framework  

## 🔧 Installation  
1. **Download** the latest release from the [Releases](https://github.com/VaultoftheForsaken/PinkCandyReturns/releases) page.  
2. **Place** `PinkCandyReturns.dll` into your `plugins` folder inside your SCP:SL server directory.  
3. **Restart your server** to apply changes.  

## 🛠️ Configuration (`config.yml`)  
After the first launch, a config file will be generated. You can edit it to modify plugin settings:  

```yaml
PinkCandyReturns:
  IsEnabled: true
  Debug: false
  PinkCandyChance: 0.1  # 10% chance for Pink Candy
