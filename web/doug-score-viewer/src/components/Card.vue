<template>
  <div class="column is-one-third-desktop is-full-touch">
    <div class="card is-relative">
      <div class="overlay has-text-centered">
        <div class="information p-4 is-flex is-flex-direction-column is-justify-content-space-between">
          <div class="top">
            <div class="is-flex is-justify-content-space-between is-align-items-center">
              <div>
                <figure class="image is-32x32">
                  <img :src="flagIconUrl" :alt="dougScore!.vehicle.originCountry" />
                </figure>
              </div>
              <div class="is-flex is-align-items-center">
                <a class="icon is-red" :href="dougScore!.videoLink" target="_blank">
                  <i class="fa-brands fa-lg fa-fw fa-youtube"></i>
                </a>
              </div>
            </div>
            <div class="is-flex">
              <div class="is-size-5">{{ dougScore!.vehicle.year }} {{ dougScore!.vehicle.make }} {{ dougScore!.vehicle.model }}</div>
            </div>
          </div>
          <div class="middle my-3">
            <div class="is-flex is-justify-content-center">
              <h1 class="title total-doug-score py-2 px-3" :class="totalDougScoreBorder">
                {{ dougScore!.totalDougScore }}
              </h1>
            </div>
          </div>
          <div class="bottom">
            <div class="is-flex is-justify-content-space-between is-align-items-center">
              <div>
                <button class="button mr-2" :class="dailyScoreBorder" @click="handleDailyScoreModal(dougScore!.dailyScore)">
                  D: {{ dougScore!.dailyScore.total }}
                </button>
                <button class="button" :class="weekendScoreBorder" @click="handleWeekendScoreModal(dougScore!.weekendScore)">
                  W: {{ dougScore!.weekendScore.total }}
                </button>
              </div>
              <div>
                <span class="icon-text">
                  <span class="icon">
                    <i class="fa-solid fa-fw fa-video"></i>
                  </span>
                  <span>{{ filmingLocation }}</span>
                </span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <Modal :is-active="isModalActive" :close-modal="closeModal" :title="title">
    <DailyScoreTable v-if="selectedDailyScore" :score="selectedDailyScore"></DailyScoreTable>
    <WeekendScoreTable v-if="selectedWeekendScore" :score="selectedWeekendScore"></WeekendScoreTable>
  </Modal>
</template>
<script setup lang="ts">
import Modal from "./Modal.vue";
import { ref, computed } from "vue";
import { DailyScore, WeekendScore } from "../models/dougScore";
import { getFlagIcon, getDougScoreBracket, getScoreBracket } from "../utils";
import DailyScoreTable from "./DailyScoreTable.vue";
import WeekendScoreTable from "./WeekendScoreTable.vue";
const { dougScore } = defineProps({
  dougScore: Object,
});

const isModalActive = ref<boolean>(false);
const selectedDailyScore = ref<DailyScore | undefined>();
const selectedWeekendScore = ref<WeekendScore | undefined>();
const title = ref<string>("");

const totalDougScoreBorder = computed(() => getDougScoreBracket(dougScore!.totalDougScore));
const dailyScoreBorder = computed(() => getScoreBracket(dougScore!.dailyScore.total));
const weekendScoreBorder = computed(() => getScoreBracket(dougScore!.weekendScore.total));
const flagIconUrl = computed(() => new URL(`../${getFlagIcon(dougScore!.vehicle.originCountry)}`, import.meta.url).href);
const filmingLocation = computed(() => `${dougScore!.filmingLocation.city}, ${dougScore!.filmingLocation.state}`);

function handleDailyScoreModal(dailyScore: DailyScore) {
  selectedDailyScore.value = dailyScore;
  isModalActive.value = !isModalActive.value;
  title.value = "View Daily Score";
}

function handleWeekendScoreModal(weekendScore: WeekendScore) {
  selectedWeekendScore.value = weekendScore;
  isModalActive.value = !isModalActive.value;
  title.value = "View Weekend Score";
}

function closeModal() {
  isModalActive.value = false;
  selectedDailyScore.value = undefined;
  selectedWeekendScore.value = undefined;
  title.value = "";
}
</script>
<style lang="scss">
.card {
  min-height: 275px;

  .total-doug-score {
    border-style: solid;
    border-width: 2px;
    border-radius: 4px;
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
