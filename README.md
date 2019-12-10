# AnalogClockWPF
アナログ時計  
  
右クリックでメニューが出ます。 
メニューから閉じる事ができます。  
メニューから設定画面が出せます。  
設定画面では、日付の表示非表示を切り替える事ができます。  
日付は、OSの設定にかかわらず、常に元号表示になります。  
１年目は「元年」と表示されます。  
  
元号は、OSから取得しています。  
OSが新しい元号の表示に対応していない場合、新しい元号の表示はできません。  
詳細は、Microsoftの発表をご確認ください。  
  
コマンドラインスイッチが使用できます。  
「/touka」背景を透過します。  
「/secHand」秒針をなめらかに動かします。  
「/minHand」分針をなめらかに動かします。  
「/date」日付を表示します。  
「/Opacity」不透明度を指定します。半角スペース後に値を指定すます。値は10-100の範囲です。  
「/size」時計の直径を指定すます。半角スペース後に値を指定します。値は50-2000の範囲です。  

サイズが大きすぎると表示がバグります。原因不明です。  
通常使用の場合、特に問題にならないので修正予定はありません。