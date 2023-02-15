mergeInto(LibraryManager.library, {

  SaveToServer: function(date) {
    var dateString = UTF8ToString(date);
    var myobj = JSON.parse(dateString);
    console.log('Saved date:', dateString);
    player.setData(myobj);
  },

  LoadFromServer: function(){
    player.getData().then(_date => {
      const myJSON = JSON.stringify(_date);
      console.log('Loaded date:', myJSON);
      return myJSON;
    });
  },

  ShowFullScreenAdv : function(){
    ysdk.adv.showFullscreenAdv({
      callbacks: {
        onError: function(error) {
          console.log('Error while open video ad:', error);
        }
      }
    })
  },

  ShowRewardedVideo : function(){
    ysdk.adv.showRewardedVideo({
      callbacks: {
        onRewarded: () => {
          myGameInstance.SendMessage('AdvertisementShower', 'GetRewardForVideo');
        },
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
      }
    })
  },

});