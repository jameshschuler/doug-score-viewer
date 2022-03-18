<template>
  <div>
    <div class="columns">
      <div class="column box is-10 is-offset-1">
        <Featured />
        <SortedDougScores sort-by="TotalDougScore" title="Top DougScores" />
        <SortedDougScores sort-by="WeekendScore" title="Top Weekend Scores" />
        <SortedDougScores sort-by="DailyScore" title="Top Daily Scores" />
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import Featured from "../components/Featured.vue";
import SortedDougScores from "../components/SortedDougScores.vue";
</script>
<style lang="scss">
.outer {
  overflow-x: scroll;
  overflow-y: hidden;
  -webkit-overflow-scrolling: touch;
  -ms-overflow-style: none; /* IE and Edge */
  scrollbar-width: none; /* Firefox */

  &::-webkit-scrollbar {
    display: none;
  }
}
</style>
