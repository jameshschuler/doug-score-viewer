<template>
  <div class="column is-one-third-desktop is-full-touch">
    <div class="card is-relative">
      <div class="overlay has-text-centered">
        <div class="information p-4 is-flex is-flex-direction-column is-justify-content-space-between">
          <div class="top">
            <div class="is-flex is-justify-content-space-between is-align-items-center">
              <div>
                <figure class="image is-32x32">
                  <img :src="getFlagIcon(dougScore.vehicle.originCountry)" :alt="dougScore.vehicle.originCountry" />
                </figure>
              </div>
              <div class="is-flex is-align-items-center">
                <span class="mr-1 is-clickable">
                  <i class="fas fa-lg fa-fw fa-map-marker-alt"></i>
                </span>
                <a class="icon is-red" :href="dougScore.videoLink" target="_blank">
                  <i class="fab fa-lg fa-fw fa-youtube"></i>
                </a>
              </div>
            </div>
            <div class="is-flex">
              <div class="is-size-5">{{ dougScore.vehicle.year }} {{ dougScore.vehicle.make }} {{ dougScore.vehicle.model }}</div>
            </div>
          </div>
          <div class="middle my-3">
            <div class="is-flex is-justify-content-center">
              <h1 class="title total-doug-score p-2 has-border-great" :class="getDougScoreBracket(dougScore.totalDougScore)">
                {{ dougScore.totalDougScore }}
              </h1>
            </div>
          </div>
          <div class="bottom">
            <div class="is-flex">
              <!-- TODO: fix hover styles -->
              <button class="button mr-2" :class="getDailyScoreBracket(dougScore.dailyScore.total)">
                {{ dougScore.dailyScore.total }}
              </button>
              <button class="button">{{ dougScore.weekendScore.total }}</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { ref } from "vue";
import { getFlagIcon, getDougScoreBracket, getDailyScoreBracket } from "../utils";
const { dougScore } = defineProps({
  dougScore: Object,
});
</script>
<style lang="scss">
.card {
  min-height: 250px;

  .total-doug-score {
    border-style: solid;
    border-width: 2px;
  }
}

.overlay {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  margin-left: auto;
  margin-right: auto;
  width: 100%;
  z-index: 2;
  height: 100%;

  .information {
    height: 100%;

    .title {
      text-overflow: ellipsis;
      overflow: hidden;
      white-space: nowrap;
      max-width: 95%;
      margin-left: auto;
      margin-right: auto;
    }
  }
}
</style>
